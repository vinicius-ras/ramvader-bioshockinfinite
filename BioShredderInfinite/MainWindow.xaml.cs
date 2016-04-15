using System.Diagnostics;
using System.Windows;

namespace BioShredderInfinite
{
	/// <summary>
	/// Implements the logic behind the trainer's #MainWindow.
	/// </summary>
	public partial class MainWindow : Window
	{
		#region PRIVATE CONSTANTS
		/// <summary>The URL which points to the webpage that runs when the user clicks the "Donate!" button of the trainer.</summary>
		private const string PROJECT_DONATION_URL = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=WJ2D2KRMPRKBS&lc=US&item_name=Supporting%20Vinicius%2eRAS%27%20open%20source%20projects&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted";
		#endregion





		#region PUBLIC METHODS
		/// <summary>
		/// Constructor.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
		}
		#endregion





		#region EVENT HANDLERS
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

		}
		#endregion
	}
}
