using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SonarQubeReSharperPluginRulesChecker
{
    class Program
    {
        static void Main()
        {
            var reSharperRules = new XmlDocument();
            reSharperRules.Load("resharperRules.xml");

            var sonarQubeRules = new XmlDocument();
            sonarQubeRules.Load("rules.xml");

            foreach (XmlNode issueType in reSharperRules.SelectNodes("/Report/IssueTypes/IssueType"))
            {
                var issueId = issueType.Attributes["Id"].Value;

                var sonarQubeIssue = sonarQubeRules.SelectSingleNode($"/rules/rule[@key = '{issueId}']");

                if (sonarQubeIssue == null)
                {
                    File.AppendAllText("diff.csv", $"{issueId};{issueType.Attributes["Category"].Value};{issueType.Attributes["Description"].Value}"+ Environment.NewLine);
                    //Console.WriteLine(issueId);
                }
            }


        }
    }
}
