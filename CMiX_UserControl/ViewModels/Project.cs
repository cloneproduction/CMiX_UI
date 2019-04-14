using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CMiX.Models;
using Memento;
using Ceras;
using Newtonsoft.Json;

namespace CMiX.ViewModels
{
    public class Project : ViewModel
    {
        public Project() : base(new ObservableCollection<Services.OSCMessenger>())
        {
            NewProjectCommand = new RelayCommand(p => NewProject());
            SaveProjectCommand = new RelayCommand(p => SaveProject());
            SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());
            OpenProjectCommand = new RelayCommand(p => OpenProject());

            ImportCompoCommand = new RelayCommand(p => ImportCompo());
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

        private Composition _selectedcomposition;
        public Composition SelectedComposition
        {
            get { return _selectedcomposition; }
            set { _selectedcomposition = value; }
        }

        public CerasSerializer Serializer { get; set; }

        public string FolderPath { get; set; }

        #region PROPERTIES
        public ICommand ImportCompoCommand { get; }
        public ICommand ExportCompoCommand { get; }

        public ICommand NewProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand SaveAsProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }

        public ICommand AddCompoCommand { get; }
        public ICommand DeleteCompoCommand { get; }
        public ICommand DuplicateCompoCommand { get; }

        public ICommand AddTabCommand { get; }
        public ICommand AddLayerCommand { get; }

        public ObservableCollection<Composition> Compositions { get; set; }
        #endregion

        #region ADD/DELETE/DUPLICATE COMPOSITION
        private void AddComposition()
        {
            Composition comp = new Composition();
            Compositions.Add(comp);
            SelectedComposition = comp;
        }

        private void DeleteComposition(object compo)
        {
            if(compo != null)
            {
                Composition comp = compo as Composition;
                Compositions.Remove(comp);
            }
        }

        private void DuplicateComposition(object compo)
        {
            if(compo != null)
            {
                Composition comp = compo as Composition;
                CompositionDTO compDTO = new CompositionDTO();
                comp.Copy(compDTO);
                Composition newcomp = new Composition();
                newcomp.Paste(compDTO);
                Compositions.Add(newcomp);
            }
        }
        #endregion

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
                    ProjectDTO projectdto = serializer.Deserialize<ProjectDTO>(data);
                    Compositions.Clear();
                    Paste(projectdto);
                    FolderPath = folderPath;
                }
            }
        }

        private void SaveProject()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            if(!string.IsNullOrEmpty(FolderPath))
            {
                ProjectDTO projectdto = new ProjectDTO();
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
                ProjectDTO projectdto = new ProjectDTO();
                this.Copy(projectdto);
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(projectdto);
                File.WriteAllBytes(folderPath, data);
                FolderPath = folderPath;
            }
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
                    CompositionDTO compositiondto = Serializer.Deserialize<CompositionDTO>(data);
                    Composition newcomp = new Composition();
                    newcomp.Paste(compositiondto);
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
                CompositionDTO compositiondto = new CompositionDTO();
                SelectedComposition.Copy(compositiondto);
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(compositiondto);
                File.WriteAllBytes(folderPath, data);
            }
        }

        #region COPY/PASTE
        public void Copy(ProjectDTO projectdto)
        {
            foreach (var comp in Compositions)
            {
                CompositionDTO compositiondto = new CompositionDTO();
                comp.Copy(compositiondto);
                projectdto.CompositionDTO.Add(compositiondto);
            }
        }

        public void Paste(ProjectDTO projectdto)
        {
            foreach (var compositiondto in projectdto.CompositionDTO)
            {
                Composition composition = new Composition();
                composition.Paste(compositiondto);
                Compositions.Add(composition);
            }
        }
        #endregion
    }
}