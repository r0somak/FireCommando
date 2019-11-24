using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Database;
using Proyecto26;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LeaderBoardScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public Text leaderboard;

        private DbHelper _db;
        void Start()
        {
            _db  = new DbHelper();
           WriteResults();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private async void WriteResults()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            
            StringBuilder res = new StringBuilder("");
            
            string result =await _db.GetLeaderboard();
            Debug.Log("JSON: "+result);
//haxior, nigdy w życiu więcej firebase
            string emailPattern = @"\""highScore\"":([0-9]+),\""userEmail\"":\""([0-9A-Za-z@.]+)\""";
            MatchCollection matches = Regex.Matches(result, emailPattern);
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Debug.LogFormat("SCORE: {0}, EMAIL: {1}", groups[1], groups[2]);
                dict.Add(groups[2].Value, Convert.ToInt32(groups[1].Value));
            }
            var myList = dict.OrderByDescending(d => d.Value).ToList();
            foreach (KeyValuePair<string, int> x in myList)
            {
                res.Append(x.Key+": "+x.Value+" Points\n");
            }
            leaderboard.text = res.ToString();
        }
    }
}
