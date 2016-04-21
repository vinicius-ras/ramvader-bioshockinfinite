/*
 * Copyright (C) 2014 Vinicius Rogério Araujo Silva
 *
 * This file is part of AquaRipper.
 * 
 * AquaRipper is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * AquaRipper is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with AquaRipper.  If not, see <http://www.gnu.org/licenses/>.
 */
using RAMvader.CodeInjection;


/* This file keeps definitions for code elements which are part of the low-level features of the trainer. */
namespace BioShredderInfinite
{
	/// <summary>Identifiers for all cheats available in the trainer.</summary>
	public enum ECheat
	{
		/// <summary>Identifies the "HP Hack" cheat.</summary>
		evCheatHPHack,
		/// <summary>Identifies the "Money Hack" cheat.</summary>
		evCheatMoneyHack,
		/// <summary>Identifies the "Salts Hack" cheat.</summary>
		evCheatSaltsHack,
		/// <summary>Identifies the "Lockpick Hack" cheat.</summary>
		evCheatLockpickHack,
		/// <summary>Identifies the "Ammo Hack" cheat.</summary>
		evCheatAmmoHack,
	}





	/// <summary>Identifiers for all of the code caves injected into the game process' memory space,
	/// once the trainer gets attached to the game.</summary>
	public enum ECodeCave
	{
		/// <summary>Identifies the code cave used for the "HP Hack" cheat.</summary>
		[CodeCaveDefinition( 0x50, 0x8B, 0x86, 0x00, 0x26, 0x00, 0x00, 0x89, 0x86, 0xFC, 0x25, 0x00, 0x00, 0x58, 0xC3 )]
		evCodeCaveHPHack,
		/// <summary>Identifies the code cave used for the "Money Hack" cheat.</summary>
		[CodeCaveDefinition( 0xB8, 0x52, 0x0E, 0x00, 0x00, 0x89, 0x41, 0x54, 0x8B, 0xC8, 0x31, 0xC0, 0xC3 )]
		evCodeCaveMoneyHack,
		/// <summary>Identifies the code cave used for the "Salts Hack" cheat.</summary>
		[CodeCaveDefinition( 0x52, 0x31, 0xD2, 0xB2, 0x64, 0x89, 0x50, 0x58, 0x5A, 0x8B, 0x40, 0x58, 0x8D, 0x8F, 0xA0, 0x41, 0x00, 0x00, 0xC3 )]
		evCodeCaveSaltsHack,
		/// <summary>Identifies the code cave used for the "Lockpicks Hack" cheat.</summary>
		[CodeCaveDefinition( 0x52, 0x31, 0xD2, 0xB2, 0x0F, 0x89, 0x50, 0x64, 0x5A, 0x83, 0xC4, 0x04, 0xC3 )]
		evCodeCaveLockpicksHack,
		/// <summary>Identifies the code cave used for the "Ammo Hack" cheat.</summary>
		[CodeCaveDefinition( 0x8B, 0x86, 0xC4, 0x07, 0x00, 0x00, 0x83, 0xF8, 0x00, 0x7C, 0x11, 0x3D, 0x00, 0x01, 0x00, 0x00, 0x7F, 0x0A, 0x31, 0xC0, 0xB0, 0x63, 0x89, 0x86, 0xC4, 0x07, 0x00, 0x00, 0xC3 )]
		evCodeCaveAmmoHack
	}





	/// <summary>Identifiers for all variables injected into the game process' memory space,
	/// once the trainer gets attached to the game.</summary>
	public enum EVariable
	{
	}
}
