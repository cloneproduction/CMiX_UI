using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CMiX.Models;
using CMiX.Services;
using Ceras;

namespace CMiX.ViewModels
{
    public class Project : ViewModel
    {
        public Project() : base(new ObservableCollection<OSCMessenger>())
        {
            Messengers = new ObservableCollection<OSCMessenger>();
            Messengers.Add(new OSCMessenger("127.0.0.1", 1111) { Name = "Default" });

            NewProjectCommand = new RelayCommand(p => NewProject());
            OpenProjectCommand = new RelayCommand(p => OpenProject());
            SaveProjectCommand = new RelayCommand(p => SaveProject());
            SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());
            QuitCommand = new RelayCommand(p => Quit(p));

            AddOSCCommand = new RelayCommand(p => AddOSC());
            RemoveSelectedOSCCommand = new RelayCommand(p => RemoveSelectedOSC());
            DeleteOSCCommand = new RelayCommand(p => DeleteOSC(p));

            ImportCompoCommand = new RelayCommand(p => ImportCompo());
            ImportCompoFromProjectCommand = new RelayCommand(p => ImportCompoFromProject());
            ExportCompoCommand = new RelayCommand(p => ExportCompo());

            AddCompoCommand = new RelayCommand(p => AddComposition());
            AddTabCommand = new RelayCommand(p => AddComposition());
            DeleteCompoCommand = new RelayCommand(p => DeleteComposition(p));

            DuplicateCompoCommand = new RelayCommand(p => DuplicateComposition(p));
            AddLayerCommand = new RelayCommand(p => AddLayer());
            Compositions = new ObservableCollection<Composition>();
            FolderPath = string.Empty;
            Serializer = new CerasSerializer();
        }

        private void ImportCompoFromProject()
        {

        }

        #region PROPERTIES
        public ICommand ImportCompoCommand { get; }
        public ICommand ImportCompoFromProjectCommand { get; }
        public ICommand ExportCompoCommand { get; }

        public ICommand NewProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand SaveAsProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand QuitCommand { get; }

        public ICommand AddOSCCommand { get; set; }
        public ICommand RemoveSelectedOSCCommand { get; set; }
        public ICommand DeleteOSCCommand { get; set; }

        public ICommand AddCompoCommand { get; }
        public ICommand DeleteCompoCommand { get; }
        public ICommand DuplicateCompoCommand { get; }

        public ICommand AddTabCommand { get; }
        public ICommand AddLayerCommand { get; }

        public ObservableCollection<Composition> Compositions { get; set; }

        public CerasSerializer Serializer { get; set; }

        public string FolderPath { get; set; }

        private Composition _selectedcomposition;
        public Composition SelectedComposition
        {
            get => _selectedcomposition;
            set => SetAndNotify(ref _selectedcomposition, value);
        }
        #endregion

        #region ADD/REMOVE/DELETE OSC
        private void DeleteOSC(object oscmessenger)
        {
            OSCMessenger messenger = oscmessenger as OSCMessenger;
            Messengers.Remove(messenger);
        }

        private void RemoveSelectedOSC()
        {

        }

        int portnumber = 0;
        private void AddOSC()
        {
            OSCMessenger oscmessenger = new OSCMessenger("127.0.0.1", 1111 + portnumber);
            Messengers.Add(oscmessenger);
            portnumber++;
        }
        #endregion

        #region ADD/DELETE/DUPLICATE COMPOSITION
        private void AddComposition()
        {
            DisabledMessages();

            Composition comp = new Composition(this.Messengers);
            Compositions.Add(comp);
            SelectedComposition = comp;

            EnabledMessages();
        }

        private void DeleteComposition(object compo)
        {
            if(compo != null)
            {
                Composition comp = compo as Composition;
                List<string> removedlayername = new List<string>();
                foreach (var layer in comp.Layers)
                {
                    removedlayername.Add(layer.LayerName);
                }
                Compositions.Remove(comp);

                QueueMessages("/LayerRemoved", removedlayername.ToArray());
                SendQueues();
            }

        }

        private void DuplicateComposition(object compo)
        {
            if(compo != null)
            {
                Composition comp = compo as Composition;
                CompositionModel compDTO = new CompositionModel();
                comp.Copy(compDTO);
                Composition newcomp = new Composition(this.Messengers);
                newcomp.Paste(compDTO);
                Compositions.Add(newcomp);
            }
        }
        #endregion

        #region MENU METHODS
        private void NewProject()
        {
            Compositions.Clear();
        }

        private void OpenProject()
        {
            System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();
            opendialog.Filter = "Project (*.cmix)|*.cmix";

            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = opendialog.FileName;
                if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
                {
                    CerasSerializer serializer = new CerasSerializer();
                    byte[] data = File.ReadAllBytes(folderPath) ;
                    ProjectModel projectmodel = serializer.Deserialize<ProjectModel>(data);
                    Compositions.Clear();
                    Paste(projectmodel);
                    FolderPath = folderPath;
                }
            }
        }

        private void SaveProject()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            if(!string.IsNullOrEmpty(FolderPath))
            {
                ProjectModel projectdto = new ProjectModel();
                this.Copy(projectdto);
                var serializer = new CerasSerializer();
                var data = serializer.Serialize(projectdto);
                File.WriteAllBytes(FolderPath, data);
            }
            else
            {
                SaveAsProject();
            }
        }

        private void SaveAsProject()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            savedialog.Filter = "Project (*.cmix)|*.cmix";
            savedialog.DefaultExt = "cmix";
            savedialog.AddExtension = true;

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectModel projectdto = new ProjectModel();
                this.Copy(projectdto);
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(projectdto);
                File.WriteAllBytes(folderPath, data);
                FolderPath = folderPath;
            }
        }

        private void Quit(object p)
        {
            var window = p as Window;
            window.Close();
        }


        private void AddLayer()
        {
            SelectedComposition.AddLayer();
        }

        private void ImportCompo()
        {
            System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();
            opendialog.Filter = "Compo (*.compmix)|*.compmix";

            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = opendialog.FileName;
                if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
                {
                    byte[] data = File.ReadAllBytes(folderPath);
                    CompositionModel compositionmodel = Serializer.Deserialize<CompositionModel>(data);
                    Composition newcomp = new Composition(this.Messengers);
                    newcomp.Paste(compositionmodel);
                    Compositions.Add(newcomp);
                }
            }
        }

        private void ExportCompo()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            savedialog.Filter = "Compo (*.compmix)|*.compmix";
            savedialog.DefaultExt = "compmix";
            savedialog.FileName = SelectedComposition.Name;
            savedialog.AddExtension = true;

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CompositionModel compositionmodel = new CompositionModel();
                SelectedComposition.Copy(compositionmodel);
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(compositionmodel);
                File.WriteAllBytes(folderPath, data);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(ProjectModel projectdto)
        {
            foreach (var comp in Compositions)
            {
                CompositionModel compositionmodel = new CompositionModel();
                comp.Copy(compositionmodel);
                projectdto.CompositionModel.Add(compositionmodel);
            }
        }

        public void Paste(ProjectModel projectdto)
        {
            foreach (var compositionmodel in projectdto.CompositionModel)
            {
                Composition composition = new Composition(this.Messengers);
                composition.Paste(compositionmodel);
                Compositions.Add(composition);
            }
        }
        #endregion
    }
}