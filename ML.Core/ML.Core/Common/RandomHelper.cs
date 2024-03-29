﻿//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;

namespace ML.Core
{
    /// <summary>
    /// 使用Random类生成伪随机数
    /// </summary>
    public class RandomHelper
    {
        //随机数对象
        private Random _random;

        /// <summary>
        /// 随机字符集
        /// </summary>
        private char[] constant =
          {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
          };

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public RandomHelper()
        {
            //为随机数对象赋值
            this._random = new Random();
        }
        #endregion

        #region 生成一个指定范围的随机整数
        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public int GetRandomInt(int minNum, int maxNum)
        {
            return this._random.Next(minNum, maxNum);
        }
        #endregion

        #region 生成一个0.0到1.0的随机小数
        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        public double GetRandomDouble()
        {
            return this._random.NextDouble();
        }
        #endregion

        #region 对一个数组进行随机排序
        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public void GetRandomArray<T>(T[] arr)
        {
            //对数组进行随机排序的算法:随机选择两个位置，将两个位置上的值交换

            //交换的次数,这里使用数组的长度作为交换次数
            int count = arr.Length;

            //开始交换
            for (int i = 0; i < count; i++)
            {
                //生成两个随机数位置
                int randomNum1 = GetRandomInt(0, arr.Length);
                int randomNum2 = GetRandomInt(0, arr.Length);

                //定义临时变量
                T temp;

                //交换两个随机数位置的值
                temp = arr[randomNum1];
                arr[randomNum1] = arr[randomNum2];
                arr[randomNum2] = temp;
            }
        }
        #endregion

        /// <summary>
        /// 根据Guid获取唯一数字序列，19位
        /// </summary>
        /// <returns></returns>
        public long LongId()
        {
            byte[] value = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(value, 0);
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 生成退款订单号
        /// </summary>
        /// <returns></returns>
        public string GetRefundOrder()
        {
            return "R" + GetCommonOrder();
        }

        /// <summary>
        /// 生成支付订单号
        /// </summary>
        /// <returns></returns>
        public string GetPayOrder()
        {
            return "P" + GetCommonOrder();
        }

        /// <summary>
        /// 生成公共的订单号
        /// </summary>
        /// <returns></returns>
        public string GetCommonOrder()
        {
            DateTimeHelper dth = new DateTimeHelper();
            string strRand = GenerateRandomNumber(5);
            int strCurTime = (int)dth.TimeSpan();
            return strRand + strCurTime.ToString();
        }
    }
}
