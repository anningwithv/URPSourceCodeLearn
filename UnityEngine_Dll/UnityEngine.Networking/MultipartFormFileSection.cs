using System;
using System.Text;

namespace UnityEngine.Networking
{
	public class MultipartFormFileSection : IMultipartFormSection
	{
		private string name;

		private byte[] data;

		private string file;

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
				return this.file;
			}
		}

		public string contentType
		{
			get
			{
				return this.content;
			}
		}

		private void Init(string name, byte[] data, string fileName, string contentType)
		{
			this.name = name;
			this.data = data;
			this.file = fileName;
			this.content = contentType;
		}

		public MultipartFormFileSection(string name, byte[] data, string fileName, string contentType)
		{
			bool flag = data == null || data.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			bool flag2 = string.IsNullOrEmpty(fileName);
			if (flag2)
			{
				fileName = "file.dat";
			}
			bool flag3 = string.IsNullOrEmpty(contentType);
			if (flag3)
			{
				contentType = "application/octet-stream";
			}
			this.Init(name, data, fileName, contentType);
		}

		public MultipartFormFileSection(byte[] data) : this(null, data, null, null)
		{
		}

		public MultipartFormFileSection(string fileName, byte[] data) : this(null, data, fileName, null)
		{
		}

		public MultipartFormFileSection(string name, string data, Encoding dataEncoding, string fileName)
		{
			bool flag = string.IsNullOrEmpty(data);
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			bool flag2 = dataEncoding == null;
			if (flag2)
			{
				dataEncoding = Encoding.UTF8;
			}
			byte[] bytes = dataEncoding.GetBytes(data);
			bool flag3 = string.IsNullOrEmpty(fileName);
			if (flag3)
			{
				fileName = "file.txt";
			}
			bool flag4 = string.IsNullOrEmpty(this.content);
			if (flag4)
			{
				this.content = "text/plain; charset=" + dataEncoding.WebName;
			}
			this.Init(name, bytes, fileName, this.content);
		}

		public MultipartFormFileSection(string data, Encoding dataEncoding, string fileName) : this(null, data, dataEncoding, fileName)
		{
		}

		public MultipartFormFileSection(string data, string fileName) : this(data, null, fileName)
		{
		}
	}
}
