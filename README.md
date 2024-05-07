![DL Count](https://img.shields.io/github/downloads/turtle-insect/DQ7/total.svg)

# 概要
3DS ドラゴンクエスト7のセーブデータ編集Tool

# ソフト
http://www.dragonquest.jp/dq7/

# 実行に必要
* Windows マシン
* .NET Framework 4.8の導入
* セーブデータの吸い出し
* セーブデータの書き戻し

# Build環境
* Windows 10(64bit)
* Visual Studio 2022

# 編集時の手順
   * 結果、以下が取得可能
      * cardinfo.bin
      * save001(save002、save003).bin
      * system.bin
* save001(save002、save003).binを読み込む
* 任意の編集を行う
* save001(save002、save003).binを書き出す
* saveDataを書き戻す
