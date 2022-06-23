using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Setting
{
	public class SettingFileXmlController : SettingFileController
	{
		//--------------------------------------------------------------------------------------------------//
		// field
		//--------------------------------------------------------------------------------------------------//

		private const string ROOT_ELEMENT_NAME = "settings";

		private string _SettingFilePath = "";
		private XDocument _SettingXml;
		private XElement _Root;

		//--------------------------------------------------------------------------------------------------//
		// method
		//--------------------------------------------------------------------------------------------------//

		public SettingFileXmlController() { }
		public SettingFileXmlController(in string _Filepath) { this.Open(_Filepath); }

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルを開く</summary>
		/// <param name="_FilePath"></param>
		public override void Open(in string _FilePath)
		{
			if (string.IsNullOrWhiteSpace(_FilePath)) throw new ArgumentException("No path entered.", nameof(_FilePath));
			if (_FilePath.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0) throw new ArgumentException("The path [" + _FilePath + "] contains invalid chars..", nameof(_FilePath));

			if (System.IO.File.Exists(_FilePath))
			{
				_SettingXml = XDocument.Load(_FilePath);
				_Root = _SettingXml.Root;
				if (_Root.Name != ROOT_ELEMENT_NAME) throw new Exception("A setting file [" + _FilePath + "] different from this application was entered.");
			}
			else
			{
				if (System.IO.Directory.Exists(_FilePath)) throw new System.IO.DirectoryNotFoundException("Not found [" + _FilePath + "].");
				try { System.IO.File.Create(_FilePath); }
				catch (Exception e) { throw new Exception("Failed create file [" + _FilePath + "].", e); }

				_Root = new XElement(ROOT_ELEMENT_NAME);
				_SettingXml = new XDocument(_Root);

				try { _SettingXml.Save(_FilePath); }
				catch (Exception e) { throw new Exception("Failed save file [" + _FilePath + "].", e); }
			}

			this._SettingFilePath = _FilePath;
		}

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルに設定を書き込む</summary>
		/// <param name="_SeetingProcess">書き込みたい設定</param>
		public override void WriteSetting(SettingProcess _SeetingProcess)
		{
			if (!(_SeetingProcess is SettingProcess))
			{
				throw new ArgumentException("Invalid type inputed. Input type: [" + _SeetingProcess.GetType().Name + "].", nameof(_SeetingProcess));
			}

			this.WriteSetting(_SeetingProcess as SettingProcessXml);
		}

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルに設定を書き込む</summary>
		/// <param name="_SeetingProcess">書き込みたい設定</param>
		public void WriteSetting(SettingProcessXml _SeetingProcess)
		{
			if (_Root is null) throw new InvalidOperationException("[" + nameof(_Root) + "] is undefined.");
			if (_SeetingProcess is null) throw new ArgumentNullException(nameof(_SeetingProcess));

			_Root.ReplaceNodes(_SeetingProcess.ReadElement());
		}

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルから設定を読み出す</summary>
		/// <param name="_SeetingProcess">読み出したい設定</param>
		public override void ReadSetting(SettingProcess _SeetingProcess)
		{
			if (!(_SeetingProcess is SettingProcess))
			{
				throw new ArgumentException("Invalid type inputed. Input type: [" + _SeetingProcess.GetType().Name + "].", nameof(_SeetingProcess));
			}

			this.ReadSetting(_SeetingProcess as SettingProcessXml);
		}

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルから設定を読み出す</summary>
		/// <param name="_SeetingProcess">読み出したい設定</param>
		public void ReadSetting(SettingProcessXml _SeetingProcess)
		{
			if (_Root is null) throw new InvalidOperationException("[" + nameof(_Root) + "] is undefined.");
			if (_SeetingProcess is null) throw new ArgumentNullException(nameof(_SeetingProcess));

			XElement _Element = _Root.Element(_SeetingProcess.SettingName);
			if (_Element is null) throw new Exception("The element [" + _SeetingProcess.SettingName + "] does not exist in the setting file.");

			_SeetingProcess.WriteElement(_Root.Element(_SeetingProcess.SettingName));
		}

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルをセーブする</summary>
		public override void Save()
		{
			if (_SettingXml is null) throw new InvalidOperationException("[" + nameof(_SettingXml) + "] is undefined.");

			try { _SettingXml.Save(this._SettingFilePath); }
			catch (Exception e) { throw new Exception("Failed save file [" + this._SettingFilePath + "].", e); }
		}

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイル閉じる</summary>
		public override void Close()
		{
			_Root = null;
			_SettingXml = null;
		}

		//--------------------------------------------------------------------------------------------------//
		// property
		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルへのパス</summary>
		public override string SettingFilePath => _SettingFilePath;
		/// <summary>デフォルトの設定ファイル名</summary>
		public override string DefaultFileName { get; set; } = "setting.xml";

		//--------------------------------------------------------------------------------------------------//
	}
}
