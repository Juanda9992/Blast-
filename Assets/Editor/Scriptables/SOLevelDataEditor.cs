using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[CustomEditor(typeof(SOLevelRules))]
public class SOLevelDataEditor : Editor
{
    private SOLevelRules inspector;
    private SliderInt rowHeight;
    private BlockType[] allBlocks = new BlockType[] { BlockType.Yellow, BlockType.Red, BlockType.Blue, BlockType.Green, BlockType.Orange };
    private BlockType leftHalf,rightHalf;
    public override VisualElement CreateInspectorGUI()
    {
        inspector = (SOLevelRules)target;
        VisualElement root = new VisualElement();

        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        SetUpRowsControl(root);

        return root;
    }

    private void SetUpRowsControl(VisualElement root)
    {
        Label rowsLabels = new Label("Quickly Add Rows");
        rowsLabels.style.marginTop = 20;
        rowsLabels.style.fontSize = 15;
        VisualElement buttonsContainer = new VisualElement();
        buttonsContainer.style.flexDirection = FlexDirection.Row;
        buttonsContainer.style.marginTop = 10;


        for (int i = 0; i < allBlocks.Length; i++)
        {
            BlockType currentBlock = allBlocks[i];
            Button colorButton = new Button();
            colorButton.text = allBlocks[i].ToString();
            colorButton.style.flexGrow = 1;


            colorButton.clicked += () => GenerateRow(currentBlock);
            buttonsContainer.Add(colorButton);
        }

        Button clearButton = new Button();
        clearButton.text = "Clear all rows";
        clearButton.clicked += ClearRows;

        rowHeight = new SliderInt("Row Height", 1, 4);
        rowHeight.showInputField = true;

        //Set up half editor for blocks
        Label halfEditorLabel = new Label("Quickly Add a row fix halfs");
        halfEditorLabel.style.fontSize = 15;
        halfEditorLabel.style.marginTop = 20;
        halfEditorLabel.style.marginBottom = 10;
        VisualElement halfsContainer = new VisualElement();
        halfsContainer.style.flexDirection = FlexDirection.Row;

        EnumField leftHalfField = new EnumField("Left Half",leftHalf);
        leftHalfField.style.flexGrow =1;
        leftHalfField.RegisterValueChangedCallback(x => leftHalf = (BlockType)x.newValue);

        EnumField rightHalfField = new EnumField("Right Half",rightHalf);
        rightHalfField.RegisterValueChangedCallback(x => rightHalf = (BlockType)x.newValue);
        rightHalfField.style.flexGrow =1;

        Button addSplicedRow = new Button(AddSplicedRow);
        addSplicedRow.text = "Add spliced Row";

        halfsContainer.Add(leftHalfField);
        halfsContainer.Add(rightHalfField);


        root.Add(rowsLabels);
        root.Add(buttonsContainer);
        root.Add(clearButton);
        root.Add(rowHeight);
        root.Add(halfEditorLabel);
        root.Add(halfsContainer);

        root.Add(addSplicedRow);
    }

    private void AddSplicedRow()
    {
        Row row = new Row();
        row.blocks = new BlockType[10];

        for(int i = 0; i< row.blocks.Length;i++)
        {
            row.blocks[i] = i<5 ? leftHalf : rightHalf;
        }

        row.layerLevel = rowHeight.value;

        inspector.rows.Add(row);
        EditorUtility.SetDirty(inspector);
    }
    private void GenerateRow(BlockType blockType)
    {
        Row row = new Row();

        row.blocks = new BlockType[10];

        for (int i = 0; i < row.blocks.Length; i++)
        {
            row.blocks[i] = blockType;
        }

        if (inspector.rows == null)
        {
            inspector.rows = new System.Collections.Generic.List<Row>();
        }
        row.layerLevel = rowHeight.value;
        inspector.rows.Add(row);
        EditorUtility.SetDirty(inspector);
    }

    private void ClearRows()
    {
        inspector.rows = new System.Collections.Generic.List<Row>();
        EditorUtility.SetDirty(inspector);
    }
}
