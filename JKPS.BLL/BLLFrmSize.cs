using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
/* -+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+-   
 * Added by : SHRISHANSHU
 * On       : 04-11-08
 * Comments : This is the file which contains the logic to autosize a Form and 
 *            a form with Report. Simply add this code to get this features 
 *            implimented to your forms.
 * -+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+-           
 *            For Reports  : prjCSS.frmsize.sizeofReport(args);
 *            For Forms    : prjCSS.frmsize.sizeofForm(args);
 * -+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+--+-+-+-+-   
 *            Where "args" can be Current Form / 'this' for IInd case
 *            and "this, panel1,reportviewer" for Reports.
 */
namespace prjCSS
{
    public class frmsize
    {
        #region SIZE OF REPORT
        public static void sizeofReport(Form frm, Panel pan, CrystalDecisions.Windows.Forms.CrystalReportViewer rptV)
        {
            frm.WindowState = FormWindowState.Maximized;
            frm.AutoSize = true;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.SizeGripStyle = SizeGripStyle.Hide;
            frm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            frm.ShowIcon = false;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.AutoScaleMode = AutoScaleMode.Dpi;
            pan.Dock = DockStyle.Bottom;
            pan.Height = (frm.Height / 4) + (frm.Height / 2);
            pan.Contains(rptV);
            rptV.Dock = DockStyle.Fill;

        }
        #endregion SIZE OF REPORT

        # region SIZE OF FORM
        public static void sizeofForm(Form frm)
        {
            frm.WindowState = FormWindowState.Maximized;
            frm.AutoSize = true;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.SizeGripStyle = SizeGripStyle.Hide;
            frm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            frm.ShowIcon = false;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.AutoScaleMode = AutoScaleMode.Dpi;
        }
        #endregion SIZE OF FORM
    }
    public class frmcss
    {
        //NOT IMPLEMENTED YET
    }
}
