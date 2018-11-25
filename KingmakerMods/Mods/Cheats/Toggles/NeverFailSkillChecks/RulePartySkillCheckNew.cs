﻿using System;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.NeverFailSkillChecks
{
	[ModifiesType]
	public class RulePartySkillCheckNew : RulePartySkillCheck
	{
		#region CONFIGURATION
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static bool IsModReady()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bNeverFailSkillChecks");
			}

			return _useMod;
		}
		#endregion

		#region DUPLICATED METHODS
		[NewMember]
		[DuplicatesBody("get_IsPassed")]
		public bool source_get_IsPassed()
		{
			throw new DeadEndException("source_get_IsPassed");
		}
		#endregion

		[ModifiesMember("IsPassed")]
		public bool mod_IsPassed
		{
			[ModifiesMember("get_IsPassed")]
			get
			{
				_useMod = IsModReady();

				if (_useMod)
				{
					return true;
				}

				return this.source_get_IsPassed();
			}
		}

		[Obsolete]
		public RulePartySkillCheckNew(StatType statType, int difficultyClass) : base(statType, difficultyClass)
		{
		}
	}
}
