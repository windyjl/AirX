using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class PopControllor {
    /*
     * 编号从0开始
     * 依次是发带、礼帽、眼镜、道符
     */
    private static readonly PopControllor instance = new PopControllor(); 
    private PopControllor() {}
    public static PopControllor Instance { get { return instance; } }
    //分别计算每种"琐事"的刷新
    /*
     * 刷新设计
     * 以十秒钟为单位
     * 预算产生数量
     * 随机排在时间序列中出现
     * 位置随机
     */
    /*
     * 数据结构
     * 下标1 - 4种分类
     * 下标2 - 出现时间（超过十秒的属于不刷新）
     */
    public double[][] popList = new double[(int)TrifleType.Max-1][];
    private float lastCreateTime = -10;
    private System.Random rand = new System.Random();

    public void CreatePopList(float timenow) {
    	//TODO 时间比例受速度影响
        if (timenow-lastCreateTime<10)
        {
            return;
        }
        lastCreateTime = timenow;
        for (int i = 0; i < (int)TrifleType.Max - 1; ++i) {
            int[] arr;
            switch ((TrifleType)i) {
                case TrifleType.Star:
                    arr = Data.Instance.getStarPopNum();
                    break;
                case TrifleType.Headband:
                    arr = Data.Instance.getHeadBandPopNum();
                    break;
                case TrifleType.BowlerHat:
                    arr = Data.Instance.getBowlerHatPopNum();
                    break;
                case TrifleType.Glasses:
                    arr = Data.Instance.getGlassesPopNum();
                    break;
                case TrifleType.Writing:
                    arr = Data.Instance.getWritingPopNum();
                    break;
                default:
                    arr = new int[2];
                    arr[0] = 3;
                    arr[1] = 7;
                    break;
            }
            popList[i] = new double[rand.Next(arr[0], arr[1])];//3-7，不包括最后一个
            for (int j = 0; j < popList[i].Length;++j ) {
                popList[i][j] = rand.NextDouble() * 10;
            }
            //排序
            var lowNums = from n in popList[i]
                          orderby n
                          select n;
            popList[i] = lowNums.ToArray();
        }
    }

    public int[] getCurruntPop(float timenow) {
        float subtime = timenow - lastCreateTime;
        string res = "";
        for (int i = 0; i < 4; ++i) {
            for (int j = 0; j < popList[i].Length; ++j) {
                if (popList[i][j] < subtime) {
                    popList[i][j] = 99; //超过10就可以了，避免重复召唤
                    res += i+",";
                }
            }
        }
        string[] list = res.Split(',');
        var unempty =
            from s in list
            where !s.Equals("")
            select s;
        list = unempty.ToArray<string>();
        int[] array = null;
        if (list.Length!=0){
            array = new int[list.Length];
            for (int i = 0; i < list.Length ; i++){
                array[i] = Int32.Parse(list[i]);
            }
        }
        return array;
    }
}