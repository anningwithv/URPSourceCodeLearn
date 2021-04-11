using System;

namespace UnityEngine.TextCore.LowLevel
{
	public enum FontEngineError
	{
		Success,
		Invalid_File_Path,
		Invalid_File_Format,
		Invalid_File_Structure,
		Invalid_File,
		Invalid_Table = 8,
		Invalid_Glyph_Index = 16,
		Invalid_Character_Code,
		Invalid_Pixel_Size = 23,
		Invalid_Library = 33,
		Invalid_Face = 35,
		Invalid_Library_or_Face = 41,
		Atlas_Generation_Cancelled = 100,
		Invalid_SharedTextureData
	}
}
