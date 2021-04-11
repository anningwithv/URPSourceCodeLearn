using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Scripting;

namespace UnityEngineInternal
{
	internal static class WebRequestUtils
	{
		private static Regex domainRegex = new Regex("^\\s*\\w+(?:\\.\\w+)+(\\/.*)?$");

		[RequiredByNativeCode]
		internal static string RedirectTo(string baseUri, string redirectUri)
		{
			bool flag = redirectUri[0] == '/';
			Uri uri;
			if (flag)
			{
				uri = new Uri(redirectUri, UriKind.Relative);
			}
			else
			{
				uri = new Uri(redirectUri, UriKind.RelativeOrAbsolute);
			}
			bool isAbsoluteUri = uri.IsAbsoluteUri;
			string absoluteUri;
			if (isAbsoluteUri)
			{
				absoluteUri = uri.AbsoluteUri;
			}
			else
			{
				Uri baseUri2 = new Uri(baseUri, UriKind.Absolute);
				Uri uri2 = new Uri(baseUri2, uri);
				absoluteUri = uri2.AbsoluteUri;
			}
			return absoluteUri;
		}

		internal static string MakeInitialUrl(string targetUrl, string localUrl)
		{
			bool flag = string.IsNullOrEmpty(targetUrl);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool prependProtocol = false;
				Uri uri = new Uri(localUrl);
				Uri uri2 = null;
				bool flag2 = targetUrl[0] == '/';
				if (flag2)
				{
					uri2 = new Uri(uri, targetUrl);
					prependProtocol = true;
				}
				bool flag3 = uri2 == null && WebRequestUtils.domainRegex.IsMatch(targetUrl);
				if (flag3)
				{
					targetUrl = uri.Scheme + "://" + targetUrl;
					prependProtocol = true;
				}
				FormatException ex = null;
				try
				{
					bool flag4 = uri2 == null && targetUrl[0] != '.';
					if (flag4)
					{
						uri2 = new Uri(targetUrl);
					}
				}
				catch (FormatException ex2)
				{
					ex = ex2;
				}
				bool flag5 = uri2 == null;
				if (flag5)
				{
					try
					{
						uri2 = new Uri(uri, targetUrl);
						prependProtocol = true;
					}
					catch (FormatException)
					{
						throw ex;
					}
				}
				result = WebRequestUtils.MakeUriString(uri2, targetUrl, prependProtocol);
			}
			return result;
		}

		internal static string MakeUriString(Uri targetUri, string targetUrl, bool prependProtocol)
		{
			bool isFile = targetUri.IsFile;
			string result;
			if (isFile)
			{
				bool flag = !targetUri.IsLoopback;
				if (flag)
				{
					result = targetUri.OriginalString;
				}
				else
				{
					string text = targetUri.AbsolutePath;
					bool flag2 = text.Contains("%");
					if (flag2)
					{
						text = WebRequestUtils.URLDecode(text);
					}
					bool flag3 = text.Length > 0 && text[0] != '/';
					if (flag3)
					{
						text = "/" + text;
					}
					result = "file://" + text;
				}
			}
			else
			{
				string scheme = targetUri.Scheme;
				bool flag4 = !prependProtocol && targetUrl.Length >= scheme.Length + 2 && targetUrl[scheme.Length + 1] != '/';
				if (flag4)
				{
					StringBuilder stringBuilder = new StringBuilder(scheme, targetUrl.Length);
					stringBuilder.Append(':');
					bool flag5 = scheme == "jar";
					if (flag5)
					{
						string text2 = targetUri.AbsolutePath;
						bool flag6 = text2.Contains("%");
						if (flag6)
						{
							text2 = WebRequestUtils.URLDecode(text2);
						}
						bool flag7 = text2.StartsWith("file:/") && text2.Length > 6 && text2[6] != '/';
						if (flag7)
						{
							stringBuilder.Append("file://");
							stringBuilder.Append(text2.Substring(5));
						}
						else
						{
							stringBuilder.Append(text2);
						}
						result = stringBuilder.ToString();
					}
					else
					{
						stringBuilder.Append(targetUri.PathAndQuery);
						stringBuilder.Append(targetUri.Fragment);
						result = stringBuilder.ToString();
					}
				}
				else
				{
					bool flag8 = targetUrl.Contains("%");
					if (flag8)
					{
						result = targetUri.OriginalString;
					}
					else
					{
						result = targetUri.AbsoluteUri;
					}
				}
			}
			return result;
		}

		private static string URLDecode(string encoded)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(encoded);
			byte[] bytes2 = WWWTranscoder.URLDecode(bytes);
			return Encoding.UTF8.GetString(bytes2);
		}
	}
}
