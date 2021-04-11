using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	[IsReadOnly, UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 2)]
	public struct ProfilerCategory
	{
		[FieldOffset(0)]
		private readonly ushort m_CategoryId;

		public string Name
		{
			get
			{
				ProfilerCategoryDescription categoryDescription = ProfilerUnsafeUtility.GetCategoryDescription(this.m_CategoryId);
				return ProfilerUnsafeUtility.Utf8ToString(categoryDescription.NameUtf8, categoryDescription.NameUtf8Len);
			}
		}

		public Color32 Color
		{
			get
			{
				return ProfilerUnsafeUtility.GetCategoryDescription(this.m_CategoryId).Color;
			}
		}

		public static ProfilerCategory Render
		{
			get
			{
				return new ProfilerCategory(0);
			}
		}

		public static ProfilerCategory Scripts
		{
			get
			{
				return new ProfilerCategory(1);
			}
		}

		public static ProfilerCategory Gui
		{
			get
			{
				return new ProfilerCategory(4);
			}
		}

		public static ProfilerCategory Physics
		{
			get
			{
				return new ProfilerCategory(5);
			}
		}

		public static ProfilerCategory Animation
		{
			get
			{
				return new ProfilerCategory(6);
			}
		}

		public static ProfilerCategory Ai
		{
			get
			{
				return new ProfilerCategory(7);
			}
		}

		public static ProfilerCategory Audio
		{
			get
			{
				return new ProfilerCategory(8);
			}
		}

		public static ProfilerCategory Video
		{
			get
			{
				return new ProfilerCategory(11);
			}
		}

		public static ProfilerCategory Particles
		{
			get
			{
				return new ProfilerCategory(12);
			}
		}

		public static ProfilerCategory Lighting
		{
			get
			{
				return new ProfilerCategory(13);
			}
		}

		public static ProfilerCategory Network
		{
			get
			{
				return new ProfilerCategory(14);
			}
		}

		public static ProfilerCategory Loading
		{
			get
			{
				return new ProfilerCategory(15);
			}
		}

		public static ProfilerCategory Vr
		{
			get
			{
				return new ProfilerCategory(22);
			}
		}

		public static ProfilerCategory Input
		{
			get
			{
				return new ProfilerCategory(30);
			}
		}

		public static ProfilerCategory Memory
		{
			get
			{
				return new ProfilerCategory(23);
			}
		}

		public static ProfilerCategory VirtualTexturing
		{
			get
			{
				return new ProfilerCategory(31);
			}
		}

		public static ProfilerCategory Internal
		{
			get
			{
				return new ProfilerCategory(24);
			}
		}

		internal static ProfilerCategory Any
		{
			get
			{
				return new ProfilerCategory(65535);
			}
		}

		public ProfilerCategory(string categoryName)
		{
			this.m_CategoryId = ProfilerUnsafeUtility.GetCategoryByName(categoryName);
		}

		internal ProfilerCategory(ushort category)
		{
			this.m_CategoryId = category;
		}

		public override string ToString()
		{
			return this.Name;
		}

		public static implicit operator ushort(ProfilerCategory category)
		{
			return category.m_CategoryId;
		}
	}
}
