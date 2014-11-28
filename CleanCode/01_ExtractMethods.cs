using System;   
using System.IO;

namespace CleanCode
{
	public static class RefactorMethod
	{
		private static void SaveData(string filePath, byte[] dataToWtiteBytes)
		{
			//open files
		    FileStream fileStreamBackup;
		    var fileStream = OpenFileStream(filePath, out fileStreamBackup);
            WriteDataToFile(dataToWtiteBytes, fileStream, fileStreamBackup);
            CloseFiles(fileStream, fileStreamBackup);
            SaveLastWriteTime(filePath);
		}

	    private static FileStream OpenFileStream(string filePath, out FileStream fileStreamBackup)
	    {
	        var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
	        fileStreamBackup = new FileStream(Path.ChangeExtension(filePath, "bkp"), FileMode.OpenOrCreate);
	        return fileStream;
	    }

	    private static void SaveLastWriteTime(string filePath)
	    {
	        var timeFilePath = filePath + ".time";
	        var fileStream = new FileStream(timeFilePath, FileMode.OpenOrCreate);
	        var t = BitConverter.GetBytes(DateTime.Now.Ticks);
	        fileStream.Write(t, 0, t.Length);
	        fileStream.Close();
	    }

	    private static void CloseFiles(FileStream fileStream, FileStream fileStreamBackup)
	    {
	        fileStream.Close();
	        fileStreamBackup.Close();
	    }

	    private static void WriteDataToFile(byte[] dataToWtiteBytes, FileStream fileStream, FileStream fileStreamBackup)
	    {
	        fileStream.Write(dataToWtiteBytes, 0, dataToWtiteBytes.Length);
	        fileStreamBackup.Write(dataToWtiteBytes, 0, dataToWtiteBytes.Length);
	    }
	}
}