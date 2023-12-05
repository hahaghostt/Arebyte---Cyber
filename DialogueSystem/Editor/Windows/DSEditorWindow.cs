using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

namespace DS.Windows
{
    using Utilities;

    public class DSEditorWindow : EditorWindow
    {
        private readonly string defaultFileName = "NewDialogue";

        private DSGraphView graphView;

        private static TextField fileNameTextField;
        private Button saveButton;

        [MenuItem("Window/Dialogue System/Dialogue Graph Window")]
        public static void Open()
        {
            DSEditorWindow window = GetWindow<DSEditorWindow>();
            window.titleContent = new GUIContent("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            fileNameTextField = DSElementUtility.CreateTextField(defaultFileName, callback =>
            {
                fileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
            }, "File Name:");

            saveButton = DSElementUtility.CreateButton("Save", () => Save());

            Button loadButton = DSElementUtility.CreateButton("Load", () => Load());
            Button clearButton = DSElementUtility.CreateButton("Clear", () => Clear());
            Button resetButton = DSElementUtility.CreateButton("Reset", () => ResetGraph());

            toolbar.Add(fileNameTextField);
            toolbar.Add(saveButton);
            toolbar.Add(loadButton);

            toolbar.Add(clearButton);
            toolbar.Add(resetButton);

            rootVisualElement.Add(toolbar);
        }

        #region Elements Addition
        private void AddGraphView()
        {
            graphView = new DSGraphView(this);

            graphView.StretchToParentSize();

            rootVisualElement.Add(graphView);
        }
        #endregion

        #region Toolbar Actions
        private void Save()
        {
            if (string.IsNullOrEmpty(fileNameTextField.value))
            {
                EditorUtility.DisplayDialog("Invalid File Name", "Please ensure the file name you've entered is valid", "OK");
                return;
            }
            DSSaveUtility.Initialize(graphView, fileNameTextField.value);
            DSSaveUtility.Save();
        }

        private void Load()
        {
            string filePath = EditorUtility.OpenFilePanel("Dialogue Graphs", "Assets/Editor/DialogueSystem/Graphs", "asset");

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            Clear();

            DSSaveUtility.Initialize(graphView, Path.GetFileNameWithoutExtension(filePath));
            DSSaveUtility.Load();
        }

        private void Clear()
        {
            graphView.ClearGraph();
        }

        private void ResetGraph()
        {
            Clear();

            UpdateFilename(defaultFileName);
        }
        #endregion

        #region Utility Methods
        public static void UpdateFilename(string newFileName)
        {
            fileNameTextField.value = newFileName;
        }

        public void SetSaving(bool setting)
        {
            saveButton.SetEnabled(setting);
        }
        #endregion
    }
}