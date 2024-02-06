using System;
using HorusApp.Abstractions;
using HorusApp.Resources;

namespace HorusApp.Models.API
{
	public class ChallengesAPI : ModelBase
	{
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int currentPoints { get; set; }
        public int totalPoints { get; set; }

        #region Vista

        public double BarProgressChallenge
        {
            get
            {
                return (PercentChallenge / 100);
            }
        }

        public string BarColorChallenger
        {
            get
            {
                return (PercentChallenge == 100) ? ResourceApp.ItemChallengeCompleteColorBar : ResourceApp.ItemChallengeIncompleteColorBar;
            }
        }

        public string ProgressColorChallenger
        {
            get
            {
                return (PercentChallenge == 100) ? ResourceApp.ItemChallengeCompleteColorProgress : ResourceApp.ItemChallengeIncompleteColorProgress;
            }
        }

        public string TaskColorChallenger
        {
            get
            {
                return (PercentChallenge == 100) ? ResourceApp.ItemChallengeCompleteColorTask : ResourceApp.ItemChallengeIncompleteColorTask;
            }
        }

        public string ArrowChallenge
        {
            get
            {
                return (PercentChallenge == 100) ? ResourceApp.arrow_right_w : ResourceApp.arrow_right_g;
            }
        }

        public string SubTitleColor
        {
            get
            {
                return (PercentChallenge == 100) ? ResourceApp.ItemChallengeCompleteColorSubtitle : ResourceApp.ItemChallengeIncompleteColorSubtitle;
            }
        }

        public string TitleColor
        {
            get
            {
                return (PercentChallenge == 100) ? ResourceApp.ItemChallengeCompleteColorTitle : ResourceApp.ItemChallengeIncompleteColorTitle;
            }
        }

        public double PercentChallenge
        {
            get
            {
                return ((currentPoints * 100) / totalPoints);
            }
        }

        public string ColorChallenge
        {
            get
            {
                return (PercentChallenge == 100) ? ResourceApp.ItemChallengeCompleteColor : ResourceApp.ItemChallengeIncompleteColor;
            }
        }
        #endregion
    }
}

