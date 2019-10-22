using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Message;
using Ceras;
using System;

namespace CMiX.ViewModels
{
    public class Project : ViewModel
    {
        public Project()
        {
            //Start Server for CerasMessenger
            Server.Start();
            Client = new Client();
            Client.Start();


            OSCMessengers = new ObservableCollection<OSCMessenger>();
            OSCMessengers.Add(new OSCMessenger("127.0.0.1", 1111) { Name = "Device (0)" });

            Compositions = new ObservableCollection<Composition>();
            EditableCompositions = new ObservableCollection<Composition>();

            FolderPath = string.Empty;
            Serializer = new CerasSerializer();

            Assets = new Assets();
            
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

            AddTabCommand = new RelayCommand(p => AddEditableComposition());

            AddEditableCompositionCommand = new RelayCommand(p => AddEditableComposition());
            RemoveEditableCompositionCommand = new RelayCommand(p => RemoveEditableComposition(p));

            AddCompositionCommand = new RelayCommand(p => AddComposition());
            DeleteSelectedCompositionCommand = new RelayCommand(p => DeleteSelectedComposition());

            DuplicateSelectedCompositionCommand = new RelayCommand(p => DuplicateSelectedComposition());
            AddLayerCommand = new RelayCommand(p => AddLayer());
            EditCompositionCommand = new RelayCommand(p => EditComposition());
            CompositionListDoubleClickCommand = new RelayCommand(p => CompositionListDoubleClick());
            SendCerasMessageCommand = new RelayCommand(p => SendCerasMessage());
        }

        private void SendCerasMessage()
        {
            if(Compositions.Count >= 1)
            {
                CompositionModel compmodel = new CompositionModel();
                Compositions[0].Copy(compmodel);
                Client.SendExampleObjects(compmodel);
            }
            
        }

        public void CompositionListDoubleClick()
        {
            System.Console.WriteLine("DoubleClickOnItem");
        }

        private void ImportCompoFromProject()
        {

        }

        #region PROPERTIES
        public Client Client { get; set; }
        public ICommand SendCerasMessageCommand { get; }


        public ICommand CompositionListDoubleClickCommand { get; }

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

        public ICommand AddEditableCompositionCommand { get; }
        public ICommand RemoveEditableCompositionCommand { get; }
        public ICommand EditCompositionCommand { get; }

        public ICommand AddCompositionCommand { get; }
        public ICommand DeleteSelectedCompositionCommand { get; }
        public ICommand DuplicateSelectedCompositionCommand { get; }

        public ICommand AddTabCommand { get; }
        public ICommand AddLayerCommand { get; }

        public ObservableCollection<Composition> Compositions { get; set; }
        public ObservableCollection<Composition> EditableCompositions { get; set; }

        public ObservableCollection<OSCMessenger> OSCMessengers { get; set; }

        public CerasSerializer Serializer { get; set; }
        public Assets Assets { get; set; }

        public string FolderPath { get; set; }

        private Composition _selectedcomposition;
        public Composition SelectedComposition
        {
            get => _selectedcomposition;
            set => SetAndNotify(ref _selectedcomposition, value);
        }

        private Composition _selectededitablecomposition;
        public Composition SelectedEditableComposition
        {
            get => _selectededitablecomposition;
            set => SetAndNotify(ref _selectededitablecomposition, value);
        }

        private OSCMessenger _selectedoscmessenger;
        public OSCMessenger SelectedOSCMessenger
        {
            get => _selectedoscmessenger;
            set => SetAndNotify(ref _selectedoscmessenger, value);
        }
        #endregion

        #region ADD/REMOVE/DELETE OSC
        int portnumber = 0;
        int oscnameid = 0;

        private void AddOSC()
        {
            OSCMessenger oscmessenger = new OSCMessenger("127.0.0.1", 1111 + portnumber);
            oscnameid++;
            oscmessenger.Name = "Device " + "(" + oscnameid.ToString() + ")";
            OSCMessengers.Add(oscmessenger);

            foreach (var compo in Compositions)
            {
                compo.OSCValidation.Add(new OSCValidation(oscmessenger));
            }
            portnumber++;
        }

        private void DeleteOSC(object oscmessenger)
        {
            var messenger = oscmessenger as OSCMessenger;
            int index = OSCMessengers.IndexOf(messenger);
            OSCMessengers.Remove(messenger);

            foreach (var compo in Compositions)
            {
                compo.OSCValidation.RemoveAt(index);
            }
        }

        private void RemoveSelectedOSC()
        {

        }
        #endregion

        #region ADD/DELETE/DUPLICATE COMPOSITION
        int CompID = 0;

        private void AddComposition()
        {
            Composition comp = new Composition(OSCMessengers, Assets);
            comp.Name = "Composition " + CompID++.ToString();
            Compositions.Add(comp);
            SelectedComposition = comp;
        }

        private void AddEditableComposition()
        {
            Composition comp = new Composition(OSCMessengers, Assets);
            comp.Name = "Composition " + CompID++.ToString();
            Compositions.Add(comp);
            SelectedComposition = comp;
            EditableCompositions.Add(comp);
            SelectedEditableComposition = comp;
        }

        private void EditComposition()
        {
            if(SelectedComposition != null && !EditableCompositions.Contains(SelectedComposition))
            {
                var compo = SelectedComposition;
                EditableCompositions.Add(compo);
                SelectedEditableComposition = compo;
            }
        }

        private void RemoveEditableComposition(object compo)
        {
            if (compo != null)
            {
                EditableCompositions.Remove(compo as Composition);
            }

            if (Compositions.Count > 0)
            {
                SelectedEditableComposition = Compositions[0];
            }
        }

        private void DeleteSelectedComposition()
        {
            if (SelectedComposition != null)
            {
                var deletedcompo = SelectedComposition as Composition;
                Compositions.Remove(deletedcompo);
                EditableCompositions.Remove(deletedcompo);
            }

            if (Compositions.Count > 0)
            {
                SelectedComposition = Compositions[0];
            }

            if (Compositions.Count == 0)
                CompID = 0;
        }

        private void DuplicateSelectedComposition()
        {
            if(SelectedComposition != null)
            {
                Composition comp = SelectedComposition as Composition;
                CompositionModel compositionmodel = new CompositionModel();
                comp.Copy(compositionmodel);
                Composition newcomp = new Composition(OSCMessengers, Assets);
                newcomp.Paste(compositionmodel);
                newcomp.Name = newcomp.Name + "- Copy";
                Compositions.Add(newcomp);
                if (newcomp.Layers[0] != null)
                    newcomp.SelectedLayer = newcomp.Layers[0];
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
                    Composition newcomp = new Composition(OSCMessengers, Assets);
                    newcomp.Paste(compositionmodel);
                    Compositions.Add(newcomp);
                    SelectedComposition = newcomp;
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
        public void Copy(ProjectModel projectmodel)
        {
            foreach (var comp in Compositions)
            {
                CompositionModel compositionmodel = new CompositionModel();
                comp.Copy(compositionmodel);
                projectmodel.CompositionModel.Add(compositionmodel);
            }
        }

        public void Paste(ProjectModel projectmodel)
        {
            foreach (var compositionmodel in projectmodel.CompositionModel)
            {
                Composition composition = new Composition(OSCMessengers, Assets);
                composition.Paste(compositionmodel);
                Compositions.Add(composition);
            }
        }
        #endregion
    }
}