//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ML.Core
{
    public class VideoHelper
    {
        //参考：https://www.jianshu.com/p/4f399b9dfb43

        /// <summary>
        /// 获取视频第一帧
        /// </summary>
        /// <param name="VideoName"></param>
        /// <param name="WidthAndHeight"></param>
        /// <param name="CutTimeFrame"></param>
        /// <returns></returns>
        public string TestGetImageByVideo(string AppPath, string VideoName, string OutImgName, string WidthAndHeight, string CutTimeFrame)
        {
            string ffmpeg = AppPath;//ffmpeg执行文件的路径
            string PicName = VideoName + ".jpg";//视频图片的名字，绝对路径
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.Arguments = " -i " + VideoName                    //视频路径
                                + " -r 1"                               //提取图片的频率
                                + " -y -frames 1 -f image2 -ss " + CutTimeFrame   //设置开始获取帧的视频时间
                                + " -t 513 -s " + WidthAndHeight             //设置图片的分辨率
                                + " " + OutImgName;  //输出的图片文件名，路径前必须有空格

            try
            {
                System.Diagnostics.Process.Start(startInfo);
                Thread.Sleep(5000);//线程挂起，等待ffmpeg截图完毕
            }
            catch (Exception)
            {
                return "";
            }


            //返回视频图片完整路径
            if (System.IO.File.Exists(OutImgName))
                return OutImgName;
            return "";
        }

        /// <summary>
        /// 视频转码
        /// </summary>
        /// <param name="AppPath"></param>
        /// <param name="VideoName"></param>
        /// <param name="OutVideoName"></param>
        /// <returns></returns>
        public string VideoTranscoding(string AppPath, string VideoName, string EncodingFormat, string OutVideoName, int Delay = 5000)
        {
            string ffmpeg = AppPath;//ffmpeg执行文件的路径
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.Arguments = $" -i {VideoName} -s 640x480 -b 500k -vcodec {EncodingFormat} -r 29.97 {OutVideoName}";  //输出的视频名，路径前必须有空格
                                                                                                                           //startInfo.Arguments = $" -i {VideoName} -vcodec libx264 -preset slower -crf 18 -threads 4 -acodec copy {OutVideoName}";

            try
            {
                System.Diagnostics.Process.Start(startInfo);
                Thread.Sleep(Delay);//线程挂起，等待ffmpeg截图完毕
            }
            catch (Exception)
            {
                return "";
            }
            //返回视频图片完整路径
            if (System.IO.File.Exists(OutVideoName))
                return OutVideoName;
            return "";
        }
    }
}
