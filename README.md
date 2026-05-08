# MyUtil : C# utility functions
自作のユーティリティ関数とその実装を公開しています  
## オススメ拡張メソッド  
MyPipe　・・　Func(値を返す関数や式)をpipeできます  
MyApply　・・　Action(値を返さない操作関数や式)を適用してpipeできます  
※MyPipeとMyApplyを使うとどこまでも処理を連結できます  
　しかしどこまでもではなく、意味のあるまとまりで区切りましょう  
　でも短い処理を繋げるには便利でオススメ  
## その他ユーティリティ拡張メソッド  
MyCountChar　・・　文字の出現回数をカウント  
MyTimes　・・　Action(値を返さない操作関数や式)を指定した回数実行  
MyGetIndex　・・　C#標準のFindIndexが-1を返すのをnullに  
MyGetLastIndex　・・　C#標準のFindLastIndexが-1を返すのをnullに   
MyIsEmpty　・・　Anyの否定、名前を付けて意図を明確にする用途  
MyForEachWithIndex　・・　インデックスを付与したリストのForEach  
MyPickDuplication　・・　リストの重複要素を取得  
MyAllCombinations　・・　リストの網羅された組み合わせをタプルで取得  
MyAdjacentCombinations　・・　リストの隣接要素との組み合わせをタプルで取得  
MyPickAny　・・　ランダムにリスト要素を取得  
MyShuffle　・・　リストをシャッフル  
MyNullable　・・　NonNull型のリストをNullable型のリストに  
MyNonNull　・・　Nullable型のリストをNonNull型のリストに  
MyMarge　・・　Dictionaryをマージして返す  
MyAdd　・・　Dictionaryに追加、キーが存在する場合なにもしない、操作後のDictionaryを返す  
MyUpdate　・・　Dictionaryのキーの値を更新、キーが存在しない場合なにもしない、操作後のDictionaryを返す　  
MyRemove　・・　Dictionaryから削除、キーが存在しない場合なにもしない、操作後のDictionaryを返す  
MyUpdate　・・　Listのインデックスの値を更新、インデックスが存在しない場合なにもしない、操作後のListを返す　  
MyPartition　・・　区切る条件関数を渡してListをListのListに分割  
MyMaybeToList　・・　classのNullableの値を要素数が1か0のリストに変換  
MyMaybeToList　・・　structのNullableの値を要素数が1か0のリストに変換  
MyAsyncForEachSequential　・・　直列で実行されるAsync処理適用のForEach  
MyAsyncForEachParallel　・・　並列で実行されるAsync処理適用のForEach  
MyAsyncForEachConcurrent　・・　並行で実行されるAsync処理適用のForEach  
MyScanStrict　・・　正格評価でAggregateの操作途中の値も含めてリストで返す  
MyScanLazy　・・　遅延評価でAggregateの操作途中の値も含めてリストで返す  
## 書いてあるが改良した方がいいかも
MyToNormalizeString　・・　ToStringのフォーマット文字列を作成、桁数指定のみ  
## C#標準入りにて削除  
MyClamp　・・　Math.ClampでC#標準入り　
MyLerp　・・　Double.LerpやSingle.LerpでC#標準入り　
## その他  
MyUtil.nothing　・・　なにもしないアクション  
MyUtil.Identity　・・　なにも変換しない関数  
MyUtil.GetFileString　・・　ファイルのテキストを安全に取得、取得できなかったらnull  
MyRandom.GenerateInt　・・　最大値と最小値指定でIntのランダム値を取得  
MyRandom.GenerateDouble　・・　最大値と最小値指定でDoubleのランダム値を取得  
MyRandom.GenerateDecimal　・・　最大値と最小値指定でDecimalのランダム値を取得  
MyRandom.RandomJudge　・・　trueの確率を指定でboolのランダム値を取得
