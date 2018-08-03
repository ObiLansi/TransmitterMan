﻿using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

using ICSharpCode.TextEditor.Document;

using IronPython.Hosting;

using Microsoft.Scripting.Hosting;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Dialogs.Scripting
{
    /// <summary>
    /// Interaktionslogik für ScriptingDialog.xaml
    /// </summary>
    public partial class ScriptingDialog : Window, INotifyPropertyChanged
    {

        /// <summary>
        /// Gets the line.
        /// </summary>
        /// <value>
        /// The line.
        /// </value>
        public int Line
        {
            get { return tecTextEditorControl.ActiveTextAreaControl.TextArea.Caret.Line + 1; }
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <value>
        /// The column.
        /// </value>
        public int Column
        {
            get { return tecTextEditorControl.ActiveTextAreaControl.TextArea.Caret.Column + 1; }
        }


        /// <summary>
        /// The string last output
        /// </summary>
        private string strLastOutput = "";

        /// <summary>
        /// Gets or sets the last output.
        /// </summary>
        /// <value>
        /// The last output.
        /// </value>
        public string LastOutput
        {
            get { return strLastOutput; }
            set
            {
                this.strLastOutput = value;
                FirePropertyChanged();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptingDialog"/> class.
        /// </summary>
        public ScriptingDialog()
        {
            InitializeComponent();

            this.DataContext = this;

            InitTextEditor();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the text editor.
        /// </summary>
        private void InitTextEditor()
        {
            this.tecTextEditorControl.HideMouseCursor = true;
            this.tecTextEditorControl.LineViewerStyle = LineViewerStyle.FullRow;
            this.tecTextEditorControl.ConvertTabsToSpaces = true;

            this.tecTextEditorControl.ShowSpaces = true;
            this.tecTextEditorControl.ShowTabs = true;
            //this.tecTextEditorControl.ShowSpaces = false;
            //this.tecTextEditorControl.ShowTabs = false;

            this.tecTextEditorControl.ShowEOLMarkers = false;
            this.tecTextEditorControl.ShowLineNumbers = true;

            this.tecTextEditorControl.IsIconBarVisible = true;
            this.tecTextEditorControl.AllowCaretBeyondEOL = true;
            this.tecTextEditorControl.AllowDrop = true;
            this.tecTextEditorControl.VRulerRow = 80;


            HighlightingManager.Manager.AddSyntaxModeFileProvider(new PythonSyntaxModeFileProvider());
            this.tecTextEditorControl.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("Python");

            //this.tecTextEditorControl.Document.FoldingManager.FoldingStrategy = sfs;
            //this.tecTextEditorControl.Document.FoldingManager.UpdateFoldings(null, null);

            string strFilename = string.Format("{0}\\HelloWorld.py", Tool.StartupPath);
            this.tecTextEditorControl.Text = File.ReadAllText(strFilename);

            this.tecTextEditorControl.ActiveTextAreaControl.TextArea.Caret.PositionChanged += Caret_PositionChanged;
            //this.tecTextEditorControl.ActiveTextAreaControl.Document.DocumentChanged += Document_DocumentChanged;

            this.Title += string.Format(" [{0}]", strFilename);
        }


        /// <summary>
        /// Executes the specified string content.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        private void Execute(string strContent)
        {
            Cursor = Cursors.Wait;

            try
            {
                //TODO: Hier muss natrülich ein eigener TextWriter her der das dann direkt in die 
                //      TextBox schreibt, das hier ist nur für das schnelle MockUp sinnvoll ...
                using (StringWriter sw = new StringWriter())
                {
                    DateTime dtStarted = DateTime.Now;
                    sw.WriteLine("[{0}] Execution Started ...", dtStarted.Fmt_DD_MM_YYYY_HH_MM_SS());
                    //Console.SetOut(TextWriter.Synchronized(sw));
                    Console.SetOut(sw);

                    ScriptEngine engine = Python.CreateEngine();
                    ScriptScope scope = engine.CreateScope();

                    engine.Runtime.IO.RedirectToConsole();
                    engine.Execute(strContent, scope);

                    //MB.Information(sw.ToString());
                    DateTime dtStopped = DateTime.Now;
                    sw.WriteLine("[{0}] Execution Ended ...", dtStopped.Fmt_DD_MM_YYYY_HH_MM_SS());
                    sw.WriteLine("[{0}] Execution Time: {1}", DateTime.Now.Fmt_DD_MM_YYYY_HH_MM_SS(),(dtStopped-dtStarted).ToShortString());

                    LastOutput = sw.ToString();

                    // Hier kann man nun die erzeugten Variablen abfragen ...
                    //StringBuilder sb = new StringBuilder();
                    //foreach (String strVariable in scope.GetVariableNames())
                    //{
                    //    sb.AppendLine(strVariable);
                    //    //var r = scope.GetVariable(strVariable);
                    //    //sb.AppendLine("{0}",strVariable,r. );
                    //}

                    //MB.Information(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            this.tecTextEditorControl.Focus();

            Cursor = Cursors.Arrow;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the PositionChanged event of the Caret control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            //tsslRow.Text = string.Format("Zeile {0}", tecTextEditorControl.ActiveTextAreaControl.TextArea.Caret.Line);
            //tsslColumn.Text = string.Format("Spalte {0}", tecTextEditorControl.ActiveTextAreaControl.TextArea.Caret.Column);
            FirePropertyChanged("Line");
            FirePropertyChanged("Column");
        }


        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.tecTextEditorControl.Focus();
        }


        /// <summary>
        /// Handles the Click event of the Button_Play control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            string strEditorContent = tecTextEditorControl.ActiveTextAreaControl.SelectionManager.HasSomethingSelected ?
                tecTextEditorControl.ActiveTextAreaControl.SelectionManager.SelectedText :
                tecTextEditorControl.Text;

            if (strEditorContent.IsNotEmpty() == true)
            {
                Execute(strEditorContent);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        protected void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }

    } // end public partial class ScriptingDialog
}
