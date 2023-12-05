using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor.UIElements;

namespace DS.Utilities
{
    using Elements;
    public static class DSElementUtility
    {
        public static ObjectField CreateTextureField(Texture2D texture, string label = "Texture")
        {
            ObjectField textureField = new ObjectField
            {
                value = texture,
                objectType = typeof(Texture2D),
                label = label,
            };

            return textureField;
        }

        public static TextField CreateTextField(string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null, string label = null)
        {
            TextField textField = new TextField()
            {
                value = value,
                label = label
            };

            if (onValueChanged != null)
            {
                textField.RegisterCallback(onValueChanged);
            }

            return textField;
        }

        public static TextField CreateTextArea(string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null, string label = null)
        {
            TextField textArea = CreateTextField(value, onValueChanged, label);

            textArea.multiline = true;

            return textArea;
        }

        public static Foldout CreateFoldout(string title, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        public static Port CreatePort(this DSNode node, string portName = "", Port.Capacity capacity = Port.Capacity.Single, Direction direction = Direction.Output, Orientation orientation = Orientation.Horizontal)
        {
            Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));

            port.portName = portName;

            return port;
        }

        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };
            return button;
        }
    }
}
