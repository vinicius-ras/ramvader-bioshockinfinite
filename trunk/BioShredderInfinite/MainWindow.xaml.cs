using RAMvader;
using RAMvader.CodeInjection;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace BioShredderInfinite
{
	/// <summary>
	/// Implements the logic behind the trainer's #MainWindow.
	/// </summary>
	public partial class MainWindow : Window
	{
		#region PRIVATE CONSTANTS
		/// <summary>The name of the process which runs the game.</summary>
		private const string GAME_PROCESS_NAME = "BioShockInfinite";
		/// <summary>The URL which points to the webpage that runs when the user clicks the "Donate!" button of the trainer.</summary>
		private const string PROJECT_DONATION_URL = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=WJ2D2KRMPRKBS&lc=US&item_name=Supporting%20Vinicius%2eRAS%27%20open%20source%20projects&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted";
		#endregion





		#region PRIVATE FIELDS
		/** The memory alteration used to inject the code cave that will keep track of the address of the player's HP variable.
		 * This memory alteration will be manually activated once the trainer injects its code into the game's memory space, and
		 * will be manually deactivated once the trainer detaches from the game. */
		private MemoryAlterationX86Call m_memAlterationHPAddressTracker;
		#endregion





		#region PUBLIC PROPERTIES
		/// <summary>An object used for performing I/O operations on the game process' memory. </summary>
		public RAMvaderTarget GameMemoryIO { get; private set; }
		/// <summary>An object used for injecting and managing code caves and arbitrary variables into the
		/// game process' memory.</summary>
		public Injector<ECheat, ECodeCave, EVariable> GameMemoryInjector { get; private set; }
		#endregion





		#region PRIVATE METHODS
		/// <summary>Called when the trainer needs to be detached from the game's process.</summary>
		private void DetachFromGame()
		{
			// Detach from the target process
			if ( GameMemoryIO.IsAttached() )
			{
				// If the game's process is still running, all cheats must be disabled
				if ( GameMemoryIO.TargetProcess.HasExited == false )
				{
					foreach ( ECheat curCheat in Enum.GetValues( typeof( ECheat ) ) )
						GameMemoryInjector.SetMemoryAlterationsActive( curCheat, false );

					m_memAlterationHPAddressTracker.SetEnabled( GameMemoryInjector, false );
				}

				// Release injected memory, cleanup and detach
				GameMemoryInjector.ResetAllocatedMemoryData();
				GameMemoryIO.DetachFromProcess();
			}
		}
		#endregion





		#region PUBLIC METHODS
		/// <summary>
		/// Constructor.
		/// </summary>
		public MainWindow()
		{
			// Initialize objects which will perform operations on the game's memory
			GameMemoryIO = new RAMvaderTarget();

			GameMemoryInjector = new Injector<ECheat, ECodeCave, EVariable>();
			GameMemoryInjector.SetTargetProcess( GameMemoryIO );

			// Initialize RAMvaderTarget
			GameMemoryIO.SetTargetEndianness( EEndianness.evEndiannessLittle );
			GameMemoryIO.SetTargetPointerSize( EPointerSize.evPointerSize32 );

			// Initialize window
			this.DataContext = null;
			InitializeComponent();
		}
		#endregion





		#region EVENT HANDLERS
		/// <summary>Called when the #MainWindow of the trainer is about to be closed (but before actually closing it).</summary>
		/// <param name="sender">Object which sent the event.</param>
		/// <param name="e">Arguments from the event.</param>
		private void WindowClosing( object sender, CancelEventArgs e )
		{
			// Detach the trainer, if it is attached
			if ( GameMemoryIO.Attached )
				DetachFromGame();
		}


		/// <summary>
		/// Called when the user clicks the "Donate!" button.
		/// </summary>
		/// <param name="sender">Object which sent the event.</param>
		/// <param name="e">Arguments from the event.</param>
		private void ButtonClickDonate( object sender, RoutedEventArgs e )
		{
			Process.Start( PROJECT_DONATION_URL );
		}


		/// <summary>
		/// Called whenever any of the CheckBoxes used to activate/deactivate cheats from the trainer gets checked or unchecked.
		/// </summary>
		/// <param name="sender">Object which sent the event.</param>
		/// <param name="e">Arguments from the event.</param>
		private void CheckBoxCheatToggled( object sender, RoutedEventArgs e )
		{
			// Only proceed if the trainer is attached...
			if ( GameMemoryIO.Attached == false )
				return;

			// Retrieve information which will be used to enable or disable the cheat
			CheckBox chkBox = (CheckBox) e.Source;
			ECheat cheatID = (ECheat) chkBox.Tag;
			bool bEnableCheat = ( chkBox.IsChecked == true );

			// Enable/disable cheat
			GameMemoryInjector.SetMemoryAlterationsActive( cheatID, bEnableCheat );
		}





		/// <summary>
		/// Called when the user clicks the "'Attach to' (or 'Detach from') game" button.
		/// </summary>
		/// <param name="sender">Object which sent the event.</param>
		/// <param name="e">Arguments from the event.</param>
		private void ButtonClickAttachToGame( object sender, RoutedEventArgs e )
		{
			// Is the trainer currently attached to the game's process?
			if ( GameMemoryIO.Attached )
				DetachFromGame();
			else
			{
				// Try to find the game's process
				Process gameProcess = Process.GetProcessesByName( GAME_PROCESS_NAME ).FirstOrDefault();
				if ( gameProcess != null )
				{
					// Try to attach to the game's process
					if ( GameMemoryIO.AttachToProcess( gameProcess ) )
					{
						// Inject the trainer's code and variables into the game's memory!
						GameMemoryInjector.Inject();

						// When the game's process exits, the MainWindow's dispatcher is used to invoke the DetachFromGame() method
						// in the same thread which "runs" our MainWindow
						GameMemoryIO.TargetProcess.EnableRaisingEvents = true;
						GameMemoryIO.TargetProcess.Exited += ( caller, args ) => {
							this.Dispatcher.Invoke( () => { this.DetachFromGame(); } );
						};

						// When trainer's code is injected into the game's memory space, always activate (manually) the code cave that keeps track of the player's HP variable's address
						IntPtr mainModuleAddress = GameMemoryIO.TargetProcess.MainModule.BaseAddress;

						m_memAlterationHPAddressTracker = new MemoryAlterationX86Call( GameMemoryIO, mainModuleAddress + 0x37D9BB, ECodeCave.evCodeCaveHPAddressTracker, 8 );
						m_memAlterationHPAddressTracker.SetEnabled( GameMemoryInjector, true );

						// Build the cheat activation steps...
						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatHPHack, new MemoryAlterationX86Call( GameMemoryIO, mainModuleAddress + 0x37DABF, ECodeCave.evCodeCaveHPHack, 7 ) );

						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatMoneyHack, new MemoryAlterationNOP( GameMemoryIO, mainModuleAddress + 0x4BED25, 3 ) );
						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatMoneyHack, new MemoryAlterationX86Call( GameMemoryIO, mainModuleAddress + 0x4B074, ECodeCave.evCodeCaveMoneyHack, 5 ) );

						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatSaltsHack, new MemoryAlterationNOP( GameMemoryIO, mainModuleAddress + 0x88079E, 3 ) );
						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatSaltsHack, new MemoryAlterationX86FarJump( GameMemoryIO, mainModuleAddress + 0x6A0048, ECodeCave.evCodeCaveSaltsHack, EJumpInstructionType.evJMP, 6 ) );

						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatLockpickHack, new MemoryAlterationNOP( GameMemoryIO, mainModuleAddress + 0x67B094, 3 ) );
						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatLockpickHack, new MemoryAlterationX86Call( GameMemoryIO, mainModuleAddress + 0x212C8B, ECodeCave.evCodeCaveLockpicksHack, 5 ) );

						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatAmmoHack, new MemoryAlterationX86Call( GameMemoryIO, mainModuleAddress + 0x887920, ECodeCave.evCodeCaveAmmoHack, 6 ) );

						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatOneHitKill, new MemoryAlterationX86Call( GameMemoryIO, mainModuleAddress + 0x9A690, ECodeCave.evCodeCaveOneHitKill1, 6 ) );
						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatOneHitKill, new MemoryAlterationX86Call( GameMemoryIO, mainModuleAddress + 0x3D2B10, ECodeCave.evCodeCaveOneHitKill2, 6 ) );

						GameMemoryInjector.AddMemoryAlteration( ECheat.evCheatSongbirdHack, new MemoryAlterationX86Call( GameMemoryIO, mainModuleAddress + 0x6CEC6D, ECodeCave.evCodeCaveSongBirdHack, 8 ) );
					}
					else
						MessageBox.Show( this,
							Properties.Resources.strMsgFailedToAttach, Properties.Resources.strMsgFailedToAttachCaption,
							MessageBoxButton.OK, MessageBoxImage.Exclamation );
				}
				else
					MessageBox.Show( this,
						Properties.Resources.strMsgGamesProcessNotFound, Properties.Resources.strMsgGamesProcessNotFoundCaption,
						MessageBoxButton.OK, MessageBoxImage.Exclamation );
			}
		}
		#endregion
	}
}
