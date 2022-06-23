namespace Setting
{
	/// <summary>設定ファイルから設定を読み出す方法を表す</summary>
	public abstract class SettingProcess
	{
		public SettingProcess() {}

		public abstract void SetSetting(in Setting _Obj);

		public abstract Setting Setting { get; }
		public abstract string SettingName { get; }
	}
}
