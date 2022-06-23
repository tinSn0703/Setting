
namespace Setting
{
	/// <summary>設定ファイルの操作を行う</summary>
	public abstract class SettingFileController
	{
		//--------------------------------------------------------------------------------------------------//
		// method
		//--------------------------------------------------------------------------------------------------//

		public SettingFileController() { }
		public SettingFileController(in string _Filepath) { this.Open(_Filepath); }

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルを開く</summary>
		/// <param name="_FilePath"></param>
		public abstract void Open(in string _FilePath);

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルに設定を書き込む</summary>
		/// <param name="_SeetingProcess">書き込みたい設定</param>
		public abstract void WriteSetting(SettingProcess _SeetingProcess);

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルから設定を読み出す</summary>
		/// <param name="_SeetingProcess">読み出したい設定</param>
		public abstract void ReadSetting(SettingProcess _SeetingProcess);

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルをセーブする</summary>
		public abstract void Save();

		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイル閉じる</summary>
		public abstract void Close();

		//--------------------------------------------------------------------------------------------------//
		// property
		//--------------------------------------------------------------------------------------------------//
		/// <summary>設定ファイルへのパス</summary>
		public abstract string SettingFilePath { get; }
		/// <summary>デフォルトの設定ファイル名</summary>
		public abstract string DefaultFileName { get; set; }

		//--------------------------------------------------------------------------------------------------//
	}
}
