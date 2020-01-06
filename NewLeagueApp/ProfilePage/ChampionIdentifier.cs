using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
namespace NewLeagueApp.ProfilePage
{
    class ChampionIdentifier
    {

        LCU.Champions champ = new LCU.Champions();


        public ChampionIdentifier()
        {
            LoadData();
        }

        private void LoadData()
        {
           

        }

        public String getChampName(int champID)
        {
            return (from somethingUnique in champ.championsInformation.Data where somethingUnique.Value.Key == champID select somethingUnique.Value.Name).Single();
        }

    }
}
     

