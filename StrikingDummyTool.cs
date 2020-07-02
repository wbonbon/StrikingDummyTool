using System;
using System.Reflection;
using Advanced_Combat_Tracker;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

// ACT will parse these assembly attributes to show plugin info in the same way it would if it were a DLL
[assembly: AssemblyTitle("StrikingDummyTool Plugin")]
[assembly: AssemblyDescription("StrikingDummyTool")]
[assembly: AssemblyVersion("1.0.0.0")]

namespace ACT_Plugin
{
    public class StrikingDummyTool : IActPluginV1 // To be loaded by ACT, plugins must implement this interface
    {
        Label statusLabel;	// Handle for the status label passed by InitPlugin()

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            statusLabel = pluginStatusText;
            statusLabel.Text = "Plugin started";
            Label lbl = new Label();
            lbl.Location = new System.Drawing.Point(8, 8);
            lbl.AutoSize = true;
            lbl.Text = "Nothing to display.";
            pluginScreenSpace.Controls.Add(lbl);
            ActGlobals.oFormActMain.ValidateLists();
            ActGlobals.oFormActMain.OnLogLineRead += oFormActMain_OnLogLineRead;

            statusLabel.Text = "Plugin Inited.";
        }

        private void oFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            string log = logInfo.logLine.Substring(15);

            //System.Diagnostics.Trace.WriteLine(log);

            Match regex = Regex.Match(log, "^00:[0-9a-f]{4}:(木人討滅戦を達成した！|木人討滅戦に失敗した……|Your trial is a success!|You have failed in your trial...|Vous avez réussi votre entraînement !|Vous avez failli à votre entraînement...|Du hast das Trainingsziel erreicht!|Du hast das Trainingsziel nicht erreicht ...)");
            if (regex.Success)
            {
                //System.Diagnostics.Trace.WriteLine("おわった");
                ActGlobals.oFormActMain.ActCommands("end");
            }
        }

        public void DeInitPlugin()  // You must unsubscribe to any events you use
        {
            statusLabel.Text = "Plugin exited";
        }
    }
}
