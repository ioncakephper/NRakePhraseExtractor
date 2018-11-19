using NRakeCore;
using System.Collections.Generic;

namespace WindowsFormsApp5
{
    public class KeyPhraseStats
    {
        public double Score { get; set; }
        public Topics Topics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public KeyPhraseStats()
        {
            Score = 0.0;
            Topics = new Topics();
        }
    }
}