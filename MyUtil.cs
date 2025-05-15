﻿namespace MyUtil {
  public static class MyUtil {
    public static async Task<string?> GetFileString(string filePath) => File.Exists(filePath) ? await filePath.MyApplyF(async name => { using StreamReader sr = new(name); return await sr.ReadToEndAsync(); }) : null;
  }
  public static class 汎用拡張メソッド {
    ///<summary>文字列中の指定した文字の出現回数をカウントする</summary>
    public static int MyCountChar(this string s,char c) => s.Length - s.Replace(c.ToString(),"").Length;
    ///<summary>アクションを指定した回数実行する</summary>
    public static void MyTimes(this Action action,int count) => Enumerable.Repeat(action,count).ToList().ForEach(action => action());
    ///<summary>見つからなかった場合にFindIndexが-1を返すのをnullに置換</summary>
    public static int? MyGetIndex<T>(this IEnumerable<T> list,Predicate<T> match) => list.ToList().FindIndex(match).MyApplyF(v => v != -1 ? v : (int?)null);
    ///<summary>見つからなかった場合にFindIndexが-1を返すのをnullに置換</summary>
    public static int? MyGetLastIndex<T>(this IEnumerable<T> list,Predicate<T> match) => list.ToList().FindLastIndex(match).MyApplyF(v => v != -1 ? v : (int?)null);
    ///<summary>!演算子を使わない形の関数に置き換え用</summary>
    public static bool MyIsEmpty<T>(this IEnumerable<T> list) => !list.Any();
    ///<summary>Funcを適用 メソッドチェーンをしたい時に有用 [例]nullじゃない場合に操作:o=o?.ApplyF(v => v.a ="a"); .NETのバージョンアップで「o=o is not null ? o.a = "a" : null;」という書き方もできるようになった</summary>
    public static T2 MyApplyF<T1, T2>(this T1 obj,Func<T1,T2> func) => func(obj);
    ///<summary>Actionを適用して返す メソッドチェーンをしたい時に有用 [例1]nullじゃない場合にメソッド呼び出し:str?.ApplyA(strList.Add);[例2]nullじゃない場合に操作:objList?.ApplyA(v=>v.clear());</summary>
    public static T MyApplyA<T>(this T obj,Action<T> action) => obj.MyApplyF(v => { action(v); return v; });
    ///<summary>decimalのToStringのフォーマット指定の文字列を作成する</summary>
    public static string MyToNormalizeString(this decimal value,int decimalPlace) => value.ToString($"0.{Enumerable.Repeat("#",decimalPlace)}");
    ///<summary>Dictionaryをマージする</summary>
    public static Dictionary<TKey,TVal> MyMarge<TKey, TVal>(this IDictionary<TKey,TVal> baseDic,Func<IEnumerable<KeyValuePair<TKey,TVal>>,KeyValuePair<TKey,TVal>> duplicateSelecter,params IDictionary<TKey,TVal>[] addDics) where TKey : notnull => new List<KeyValuePair<TKey,TVal>>([.. baseDic,.. addDics.SelectMany(v => v)]).GroupBy(kvPair => kvPair.Key,(_,kvPairs) => duplicateSelecter(kvPairs)).ToDictionary(kvPair => kvPair.Key,kvPair => kvPair.Value);
    ///<summary>Listの重複要素を検出する</summary>
    public static List<T> MyPickDuplication<T>(this IEnumerable<T> list,params IEnumerable<T>[] otherList) => [.. otherList.Aggregate(list,(sum,next) => sum.Concat(next)).GroupBy(name => name).Where(name => name.Count() > 1).Select(group => group.Key)];
    ///<summary>Listを要素数でグループ分けする</summary>
    public static List<List<T>> MyToChunks<T>(this IEnumerable<T> list,int chunkSize) => [.. list.Select((value,index) => (value, index)).GroupBy(item => item.index / chunkSize,item => item.value).Select(chunkGroup => chunkGroup.ToList())];
    ///<summary>リスト内の網羅された組み合わせをタプルで取得する 後ろの要素ほど長い組み合わせの2次元リストを結合したもの ＊遅延取得に改造も念頭においているため</summary>
    public static List<(T, T)> MyAllCombinations<T>(this IEnumerable<T> list) => [.. list.Skip(1).SelectMany((elem,index) => list.Take(index + 1).Select(elem2 => (elem, elem2)))];
    ///<summary>リスト内の隣接要素との組み合わせをタプルで取得する</summary>
    public static List<(T, T)> MyAdjacentCombinations<T>(this IEnumerable<T> list) => [.. list.SkipLast(1).Select((elem,index) => (elem, list.ElementAt(index + 1)))];
    ///<summary>リストからランダムに要素を取得する</summary>
    public static T MyPickAny<T>(this IEnumerable<T> list) => list.ElementAt(MyRandom.GenerateInt(0,list.Count()));
    ///<summary>リストをシャッフルする</summary>
    public static List<T> MyShuffle<T>(this IEnumerable<T> list) => [.. list.OrderBy(i => Guid.NewGuid())];
    ///<summary>NonNullなリストをNullableなリストにする</summary>
    public static List<T?> MyNullable<T>(this IEnumerable<T> list) where T : struct => [.. list.OfType<T?>()];
    ///<summary>NullableなリストをNonNullなリストにする null要素は除外される</summary>
    public static List<T> MyNonNull<T>(this IEnumerable<T?> list) where T : class => [.. list.OfType<T>()];
    ///<summary>NullableなリストをNonNullなリストにする null要素は除外される</summary>
    public static List<T> MyNonNull<T>(this IEnumerable<T?> list) where T : struct => [.. list.OfType<T>()];
    ///<summary>Dictionaryにキーが存在しない場合のみ追加操作、操作後の値を返す</summary>
    public static Dictionary<T1,T2> MyAdd<T1, T2>(this Dictionary<T1,T2> map,T1 key,T2 newValue) where T1 : notnull => map.ToDictionary().MyApplyF(map => { map.TryAdd(key,newValue); return map; });
    ///<summary>Dictionaryにキーが存在する場合のみ更新操作、操作後の値を返す</summary>
    public static Dictionary<T1,T2> MyUpdate<T1, T2>(this Dictionary<T1,T2> map,T1 key,Func<T1,T2,T2> newValueFactory) where T1 : notnull => map.TryGetValue(key,out T2? value) ? map.ToDictionary().MyApplyA(v => v[key] = newValueFactory(key,value)) : map;
    ///<summary>Dictionaryにキーが存在する場合のみ削除操作、操作後の値を返す</summary>
    public static Dictionary<T1,T2> MyRemove<T1, T2>(this Dictionary<T1,T2> map,T1 key) where T1 : notnull => map.ToDictionary().MyApplyF(map => { map.Remove(key); return map; });
    ///<summary>Listに指定インデックスが有効な場合のみ更新操作、操作後の値を返す</summary>
    public static List<T> MyUpdate<T>(this List<T> list,int index,Func<T,T> newValueFactory) => (0 <= index && index < list.Count) ? list.ToList().MyApplyA(list => list[index] = newValueFactory(list[index])) : list;
    ///<summary>ListをListのListに分割して返す、分割する区切り箇所を決める条件関数を渡す</summary>
    public static List<List<T>> MyPartition<T>(this IEnumerable<T> list,Func<T,bool> cond) => list.Aggregate(new List<List<T>>(),(fold,elem) => cond(elem) ? [.. fold.Append([elem])] : [.. fold.SkipLast(1).Append([.. fold.Last().Append(elem)])]);
    ///<summary>Nullableの値を要素数が1か0のリストに変換する</summary>
    public static List<T> MyMaybeToList<T>(this T? maybeElem) where T : class => maybeElem != null ? [maybeElem] : [];
    ///<summary>Nullableの値を要素数が1か0のリストに変換する</summary>
    public static List<T> MyMaybeToList<T>(this T? maybeElem) where T : struct => maybeElem != null ? [maybeElem.Value] : [];
    ///<summary>並列で実行されるAsync処理適用のForEach</summary>
    public static async Task MyAsyncForEachParallel<T>(this IEnumerable<T> list,Func<T,Task> asyncAction) => await Task.WhenAll(list.Select(elem => Task.Run(async () => await asyncAction(elem))));
    ///<summary>直列で実行されるAsync処理適用のForEach</summary>
    public static void MyAsyncForEachSequential<T>(this IEnumerable<T> list,Func<T,Task> asyncAction) => list.ToList().ForEach(elem => asyncAction(elem).RunSynchronously());
    ///<summary>操作途中の値も含めてリストで返す</summary>
    public static IEnumerable<T2> MyScan<T1, T2>(this IEnumerable<T1> input,Func<T2,T1,T2> next,T2 state) => [.. input.Aggregate(new List<T2> { state },(stateList,item) => [.. stateList.Append(next(stateList.Last(),item))])];
    ///<summary>操作をしながら操作途中の値も含めてリストで返す</summary>
    public static IEnumerable<T2> MyScanLazy<T1, T2>(this IEnumerable<T1> input,Func<T2,T1,T2> next,T2 state) { yield return state; foreach(var item in input) { state = next(state,item); yield return state; } }
  }
  public class MyRandom {
    private static readonly Random rnd = new();
    ///<summary>整数値の乱数を取得</summary>
    public static int GenerateInt(int minValue = 0,int maxValue = int.MaxValue) => rnd.Next(minValue,maxValue);
    ///<summary>実数値(double float)の乱数を取得</summary>
    public static double GenerateDouble(double minValue = 0,double maxValue = 1) => rnd.NextDouble() * (maxValue - minValue) + minValue;
    ///<summary>実数値(decimal)の乱数を取得</summary>
    public static decimal GenerateDecimal(decimal minValue = 0,decimal maxValue = 1) => (decimal)rnd.NextDouble() * (maxValue - minValue) + minValue;
    ///<summary>bool値の乱数を取得 trueの確率を指定 0.5を設定で半々の確率</summary>
    public static bool RandomJudge(double probability) => GenerateDouble() < probability;
  }
}