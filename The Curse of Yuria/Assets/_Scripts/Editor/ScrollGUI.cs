using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Scroll))]
public class ScrollGUI : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        var item = (Scroll)target;

        if (item == null || item.icon == null)
        {
            return null;
        }

        var texture = new Texture2D(width, height);
        EditorUtility.CopySerialized(source: item.icon.texture, dest: texture);
        return texture;
    }
}
