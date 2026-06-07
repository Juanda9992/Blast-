using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[CustomEditor(typeof(SOLevelRules))]
public class SOLevelDataEditor : Editor
{
    private SOLevelRules inspector;
    private SliderInt rowHeight;
    public override VisualElement CreateInspectorGUI()
    {
        inspector = (SOLevelRules) target;
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

        BlockType[] allBlocks = new BlockType[] { BlockType.Yellow, BlockType.Red, BlockType.Blue, BlockType.Green, BlockType.Orange };

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

        rowHeight = new SliderInt("Row Height",1,4);
        rowHeight.showInputField = true;

        root.Add(rowsLabels);
        root.Add(buttonsContainer);
        root.Add(clearButton);
        root.Add(rowHeight);
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
