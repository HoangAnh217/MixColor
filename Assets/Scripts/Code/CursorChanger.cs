using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    // Biến lưu trữ texture của con trỏ mới
    public Texture2D cursorTexture;

    // Biến lưu vị trí "hotspot" của con trỏ (điểm trên ảnh sẽ tương ứng với vị trí chính xác của con trỏ)
    public Vector2 hotspot = Vector2.zero;

    // Chế độ hiển thị con trỏ
    public CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        ChangeCursor();
    }

    public void ChangeCursor()
    {
        // Thay đổi con trỏ chuột với hình ảnh và thông số đã thiết lập
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }

    public void ResetCursor()
    {
        // Đặt lại con trỏ chuột về mặc định
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
