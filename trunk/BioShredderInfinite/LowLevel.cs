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
using System;


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
		/// <summary>Identifies the "One Hit Kill" cheat.</summary>
		evCheatOneHitKill,
	}





	/// <summary>Identifiers for all of the code caves injected into the game process' memory space,
	/// once the trainer gets attached to the game.</summary>
	public enum ECodeCave
	{
		/// <summary>Identifies the code cave used for keeping the #EVariable.evVarPlayerHPAddress value always up-to-date with the address of the
		/// player's HP variable.</summary>
		[CodeCaveDefinition( 0xF3, 0x0F, 0x10, 0x8E, 0x04, 0x26, 0x00, 0x00, 0x50, 0x8D, 0x86, 0xFC, 0x25, 0x00, 0x00, 0x89,
			0x05, EVariable.evVarPlayerHPAddress, 0x58, 0xC3 )]
		evCodeCaveHPAddressTracker,
		/// <summary>Identifies the second code cave used for the "HP Hack" cheat.</summary>
		[CodeCaveDefinition( 0xD9, 0x86, 0x00, 0x26, 0x00, 0x00, 0x50, 0x31, 0xC0, 0xB0, 0x64, 0x50, 0xDB, 0x04, 0x24, 0x58,
			0xDE, 0xE9, 0xD9, 0x9E, 0xFC, 0x25, 0x00, 0x00, 0x58, 0x0F, 0x2E, 0x8E, 0x04, 0x26, 0x00, 0x00, 0xC3 )]
		evCodeCaveHPHack,
		/// <summary>Identifies the code cave used for the "Money Hack" cheat.</summary>
		[CodeCaveDefinition( 0xB8, 0x52, 0x0E, 0x00, 0x00, 0x89, 0x41, 0x54, 0x8B, 0xC8, 0x31, 0xC0, 0xC3 )]
		evCodeCaveMoneyHack,
		/// <summary>Identifies the code cave used for the "Salts Hack" cheat.</summary>
		[CodeCaveDefinition( 0x31, 0xC0, 0xB0, 0x5A, 0x89, 0x41, 0x58, 0xC2, 0x04, 0x00 )]
		evCodeCaveSaltsHack,
		/// <summary>Identifies the code cave used for the "Lockpicks Hack" cheat.</summary>
		[CodeCaveDefinition( 0x52, 0x31, 0xD2, 0xB2, 0x0F, 0x89, 0x50, 0x64, 0x5A, 0x83, 0xC4, 0x04, 0xC3 )]
		evCodeCaveLockpicksHack,
		/// <summary>Identifies the code cave used for the "Ammo Hack" cheat.</summary>
		[CodeCaveDefinition( 0x8B, 0x86, 0xC4, 0x07, 0x00, 0x00, 0x83, 0xF8, 0x00, 0x7C, 0x11, 0x3D, 0x00, 0x01, 0x00, 0x00, 0x7F, 0x0A, 0x31, 0xC0,
			0xB0, 0x63, 0x89, 0x86, 0xC4, 0x07, 0x00, 0x00, 0xC3 )]
		evCodeCaveAmmoHack,
		/// <summary>Identifies the code cave used for the "One Hit Kill" cheat (deals with humanoids).</summary>
		[CodeCaveDefinition( 0x50, 0x52, 0xA1, EVariable.evVarPlayerHPAddress, 0x8D, 0x91, 0x5C, 0x22, 0x00, 0x00, 0x39, 0xD0, 0x74, 0x20, 0x31, 0xC0,
			0x40, 0x50, 0xDB, 0x04, 0x24, 0x58, 0xD9, 0x81, 0x5C, 0x22, 0x00, 0x00, 0xD8, 0xD9, 0x9B, 0xDF, 0xE0, 0x9E, 0x72, 0x08, 0xD9, 0x99, 0x5C,
			0x22, 0x00, 0x00, 0xEB, 0x02, 0xDD, 0xD8, 0x5A, 0x58, 0xD9, 0x81, 0x5C, 0x22, 0x00, 0x00, 0xC3 )]
		evCodeCaveOneHitKill1,
		/// <summary>Identifies the code cave used for the "One Hit Kill" cheat (deals with turrets and robots).</summary>
		[CodeCaveDefinition( 0x50, 0x52, 0xB8, 0x80, 0x38, 0x01, 0x00, 0x50, 0xDB, 0x04, 0x24, 0x58, 0xD9, 0x81, 0xB0, 0x04, 0x00, 0x00, 0xDE, 0xD9,
			0x9B, 0xDF, 0xE0, 0x9E, 0x72, 0x0C, 0x8D, 0x81, 0xB0, 0x04, 0x00, 0x00, 0x89, 0x05, EVariable.evVarComstockZeppelingHP, 0x8D, 0x81, 0xB0,
			0x04, 0x00, 0x00, 0x8B, 0x15, EVariable.evVarComstockZeppelingHP, 0x39, 0xD0, 0x74, 0x1E, 0x31, 0xC0, 0x40, 0x50, 0xDB, 0x04, 0x24, 0x58,
			0xD9, 0x81, 0xB0, 0x04, 0x00, 0x00, 0xD8, 0xD9, 0x9B, 0xDF, 0xE0, 0x9E, 0x72, 0x06, 0xD9, 0x91, 0xB0, 0x04, 0x00, 0x00, 0xDD, 0xD8, 0x5A,
			0x58, 0xD9, 0x81, 0xB0, 0x04, 0x00, 0x00, 0xC3 )]
		evCodeCaveOneHitKill2,
	}





	/// <summary>Identifiers for all variables injected into the game process' memory space,
	/// once the trainer gets attached to the game.</summary>
	public enum EVariable
	{
		/** Identifies the variable that keeps the player HP variable's address in the game's process.
		 * Used for the #ECheat.evCheatHPHack cheat. */
		[VariableDefinition( (Int32) 0 )]
		evVarPlayerHPAddress,
		/** Identifies the variable that keeps Comstock Zeppelin HP variable's address in the game's process.
		 * Used for the #ECheat.evCheatOneHitKill cheat. */
		[VariableDefinition( (Single) 0 )]
		evVarComstockZeppelingHP,
	}
}
