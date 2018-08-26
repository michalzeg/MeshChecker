using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using IncoherentMeshChecker.Converter;
using System.Threading.Tasks;
using System.Threading;
using IncoherentMeshChecker.Shared.Helpers;
using IncoherentMeshChecker.App.Tables;
using IncoherentMeshChecker.Calculations.Calculations;

namespace IncoherentMeshChecker.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        private const string defaultRadious = "5";

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            this.PasteNodeTable = new RelayCommand(this.pasteNodeTable);
            this.PasteElementTable = new RelayCommand(this.pasteElementTable);
            this.Cancel = new RelayCommand(this.cancel, this.canCancel);
            this.RunCalculations = new RelayCommand(this.runCalculations, this.canRunCalculations);
            this.radious = defaultRadious;
            this.progresIndicatior = new Progress<ProgressArgument>(this.reportProgress);
        }

        private bool validationSuspended = true;
        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    this.Cancel.RaiseCanExecuteChanged();
                }
            }
        }

        private CancellationTokenSource cancellationTokenSource;

        private ICollection<string> resultText;

        public ICollection<string> ResultText
        {
            get { return resultText; }
            set
            {
                if (value != this.resultText)
                {
                    resultText = value;
                    RaisePropertyChanged(() => this.ResultText);
                }
            }
        }

        private string statusBarText;

        public string StatusBarText
        {
            get { return statusBarText; }
            set
            {
                if (value != this.statusBarText)
                {
                    statusBarText = value;
                    RaisePropertyChanged(() => this.StatusBarText);
                }
            }
        }

        private bool nodeTableValidated = true;
        private ICollection<NodeTable> nodes;

        public ICollection<NodeTable> Nodes
        {
            get { return nodes; }
            set
            {
                if (value != this.nodes)
                {
                    this.nodes = value;
                    RaisePropertyChanged(() => this.Nodes);
                }
            }
        }

        private bool elementTableValidated = true;
        private ICollection<ElementTable> elements;

        public ICollection<ElementTable> Elements
        {
            get { return elements; }
            set
            {
                if (value != elements)
                {
                    elements = value;
                    RaisePropertyChanged(() => Elements);
                }
            }
        }

        private bool radiousValidated = true;
        private string radious;

        public string Radious
        {
            get { return radious; }
            set
            {
                if (value != radious)
                {
                    radious = value;
                    RaisePropertyChanged(() => Radious);
                }
            }
        }

        private int progress;

        public int Progress
        {
            get { return this.progress; }
            set
            {
                if (value != this.progress)
                {
                    this.progress = value;
                    RaisePropertyChanged(() => this.Progress);
                }
            }
        }

        private string progressText;

        public string ProgressText
        {
            get { return this.progressText; }
            set
            {
                if (value != this.progressText)
                {
                    this.progressText = value;
                    RaisePropertyChanged(() => this.ProgressText);
                }
            }
        }

        public RelayCommand PasteNodeTable { get; private set; }

        private async void pasteNodeTable()
        {
            if (checkIfBusy())
                return;
            this.cancellationTokenSource = new CancellationTokenSource();
            this.nodeTableValidated = false;

            IDataObject dataInClipboard = Clipboard.GetDataObject();
            this.StatusBarText = "Current operation: Pasting node data to the table";
            this.Progress = 100;

            if (!TableHeaderValidation.ValidateNodeTableHeaders(dataInClipboard))
                return;

            this.Nodes = await GetNodeTableData(dataInClipboard);

            this.IsBusy = false;
            validationSuspended = false;
            this.StatusBarText = "";
            this.Progress = 0;
            this.RunCalculations.RaiseCanExecuteChanged();
        }

        private async Task<IList<NodeTable>> GetNodeTableData(IDataObject dataInClipboard)
        {
            IList<NodeTable> table = new List<NodeTable>();
            var pasting = new PasteToDataGridView(this.cancellationTokenSource.Token);
            bool result;
            this.IsBusy = true;
            try
            {
                result = await Task.Run(() => pasting.PasteNodeTable(ref table, dataInClipboard));

                if (result)
                {
                    this.nodeTableValidated = true;
                }
                else
                {
                    this.nodeTableValidated = false;
                    this.ResultText = pasting.Errors;
                }
            }
            catch (OperationCanceledException)
            {
                this.nodeTableValidated = false;
            }

            return table;
        }

        public RelayCommand PasteElementTable { get; private set; }

        private async void pasteElementTable()
        {
            if (checkIfBusy())
                return;
            this.cancellationTokenSource = new CancellationTokenSource();
            this.elementTableValidated = false;

            IDataObject dataInClipboard = Clipboard.GetDataObject();
            this.StatusBarText = "Current operation: Pasting element data to the table";
            this.Progress = 100;
            if (!TableHeaderValidation.ValidateElementTableHeaders(dataInClipboard))
                return;

            this.Elements = await GetTableElementData(dataInClipboard);
            validationSuspended = false;
            this.IsBusy = false;
            this.StatusBarText = "";
            this.Progress = 0;
            this.RunCalculations.RaiseCanExecuteChanged();
        }

        private async Task<ICollection<ElementTable>> GetTableElementData(IDataObject dataInClipboard)
        {
            ICollection<ElementTable> table = new List<ElementTable>();
            PasteToDataGridView pasting = new PasteToDataGridView(this.cancellationTokenSource.Token);
            bool result;
            this.IsBusy = true;
            try
            {
                result = await Task<bool>.Run<bool>(() => pasting.PasteElementTable(ref table, dataInClipboard));

                if (result)
                {
                    this.elementTableValidated = true;
                }
                else
                {
                    this.elementTableValidated = false;
                    this.ResultText = pasting.Errors;
                }
            }
            catch (OperationCanceledException)
            {
                this.elementTableValidated = false;
            }

            return table;
        }

        public RelayCommand RunCalculations { get; private set; }

        private async void runCalculations()
        {
            if (checkIfBusy())
                return;
            this.cancellationTokenSource = new CancellationTokenSource();
            TableDataConverter converter = new TableDataConverter(this.elements, this.nodes, this.cancellationTokenSource.Token);
            this.IsBusy = true;
            this.StatusBarText = "Preparing data...";
            var startTime = DateTime.Now;
            try
            {
                this.Progress = 100;
                await Task.Run(() => converter.Convert());
                if (!converter.HasErrors)
                {
                    double radious = double.Parse(this.radious);
                    var checker = new IncoherentnessChecker(converter.Elements, converter.Nodes, radious, this.progresIndicatior, this.cancellationTokenSource.Token);
                    this.ResultText = await Task.Run(() => checker.FindIncoherentNodes());
                }
                else
                {
                    this.ResultText = converter.ErrorList;
                }
            }
            catch (OperationCanceledException)
            {
                this.operationCancelled();
            }

            var endTime = DateTime.Now;
            this.IsBusy = false;
        }

        private bool canRunCalculations()
        {
            bool result = false;

            if (validationSuspended)
                result = false;
            else if (this.Nodes == null || this.Nodes.Count == 0)
                result = false;
            else if (this.Elements == null || this.Elements.Count == 0)
                result = false;
            else if (this.elementTableValidated && this.nodeTableValidated && this.radiousValidated)
                result = true;
            return result;
        }

        public RelayCommand Cancel { get; private set; }

        private void cancel()
        {
            this.cancellationTokenSource.Cancel();
        }

        private bool canCancel()
        {
            return isBusy;
        }

        private void operationCancelled()
        {
            this.StatusBarText = "Operation has been canceled";
            this.ProgressText = "";
            this.Progress = 0;
        }

        private bool checkIfBusy()
        {
            if (this.IsBusy)
            {
                MessageBox.Show("Another operation is being performed", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            return false;
        }

        private IProgress<ProgressArgument> progresIndicatior;

        private void reportProgress(ProgressArgument progress)
        {
            this.Progress = progress.Progress;
            this.ProgressText = string.Format("{0}%", progress.Progress);
            this.StatusBarText = string.Format("Current element: {0}", progress.Message);
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                string elementsName = PropertyName.GetPropertyName(() => this.Elements);
                string nodesName = PropertyName.GetPropertyName(() => this.Nodes);
                string radiousName = PropertyName.GetPropertyName(() => this.Radious);

                if (columnName == elementsName)
                    error = (!this.elementTableValidated) ? "Wrong table data" : string.Empty;
                else if (columnName == nodesName)
                    error = (!this.nodeTableValidated) ? "Wrong table data" : string.Empty;
                else if (columnName == radiousName)
                {
                    double p;
                    this.radiousValidated = double.TryParse(radious, out p);
                    error = (!this.radiousValidated) ? "Please enter a number" : string.Empty;
                }
                this.RunCalculations.RaiseCanExecuteChanged();
                return error;
            }
        }
    }
}