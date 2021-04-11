using System;
using System.Text;

namespace UnityEngine.Networking
{
	public class MultipartFormDataSection : IMultipartFormSection
	{
		private string name;

		private byte[] data;

		private string content;

		public string sectionName
		{
			get
			{
				return this.name;
			}
		}

		public byte[] sectionData
		{
			get
			{
				return this.data;
			}
		}

		public string fileName
		{
			get
			{
				return null;
			}
		}

		public string contentType
		{
			get
			{
				return this.content;
			}
		}

		public MultipartFormDataSection(string name, byte[] data, string contentType)
		{
			bool flag = data == null || data.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			this.name = name;
			this.data = data;
			this.content = contentType;
		}

		public MultipartFormDataSection(string name, byte[] data) : this(name, data, null)
		{
		}

		public MultipartFormDataSection(byte[] data) : this(null, data)
		{
		}

		public MultipartFormDataSection(string name, string data, Encoding encoding, string contentType)
		{
			bool flag = string.IsNullOrEmpty(data);
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			byte[] bytes = encoding.GetBytes(data);
			this.name = name;
			this.data = bytes;
			bool flag2 = contentType != null && !contentType.Contains("encoding=");
			if (flag2)
			{
				contentType = contentType.Trim() + "; encoding=" + encoding.WebName;
			}
			this.content = contentType;
		}

		public MultipartFormDataSection(string name, string data, string contentType) : this(name, data, Encoding.UTF8, contentType)
		{
		}

		public MultipartFormDataSection(string name, string data) : this(name, data, "text/plain")
		{
		}

		public MultipartFormDataSection(string data) : this(null, data)
		{
		}
	}
}
