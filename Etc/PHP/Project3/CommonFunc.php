<?php
ini_set( 'display_errors', 1 );
ini_set( 'error_reporting', E_ALL );

function GetPDO() {
    $db_user = "al_user_2023_9gatsu_00";	// ユーザー名
    $db_pass = "9.X9m8H-tP8vol@t";	// パスワード
    $db_host = "localhost";	// ホスト名
    $db_name = "ACTIVE_LEARNING_2023_9GATSU_00";	// データベース名
    $db_type = "mysql";	// データベースの種類

    $dsn = "$db_type:host=$db_host;dbname=$db_name;charset=utf8";

    // データベースへの接続を確認
    $pdo = null;
	try {
        $pdo = new PDO($dsn, $db_user,$db_pass);
        $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $pdo->setAttribute(PDO::ATTR_EMULATE_PREPARES, false);
	} catch ( PDOException $e ) {
		print "接続エラー:{$e->getMessage()}";
	}

    return $pdo;
}

function GetRowFromUserData($input_userId){
    $pdo = GetPDO();

    if($pdo == null){
        print("PDOが取得できませんでした");
        exit(0);
    }

    $sql= "SELECT * FROM user_data WHERE Id =".$input_userId;
    $stmh = $pdo->prepare($sql);
    $stmh->execute();
    $row = $stmh->fetch(PDO::FETCH_ASSOC);

    if($row == null){
//            print("当該のIDのユーザが見つからなかったので、レコードを作成しました.\n");
        $sql= "INSERT INTO user_data (Id,UserName,UserHasCharaFlag) VALUES (".$input_userId.",'NONAME',0)";
        $stmh = $pdo->prepare($sql);
        $stmh->execute();

        $sql= "SELECT * FROM user_data WHERE Id =".$input_userId;
        $stmh = $pdo->prepare($sql);
        $stmh->execute();
        $row = $stmh->fetch(PDO::FETCH_ASSOC);
    }

    return $row;
}

function PlayGacha($input_userId, $gachaCount){
    $pdo = GetPDO();

    if($pdo == null){
        print("PDOが取得できませんでした");
        exit(0);
    }

    try {
        $row = GetRowFromUserData($input_userId);
        if($row == null){
            print("user_dataテーブルからレコードが取得できませんでした");
            exit(0);
        }

        $has_chara_flag = $row ['UserHasCharaFlag'];
        $result_string = "";
        for ($i = 1; $i <= $gachaCount; $i++) {
            $rand_num = mt_rand(1, 10000);
            $jouyo = $rand_num % 32;
            $result_string = $result_string.$jouyo.",";
            $jouyo_flag = 1 << $jouyo;
            $has_chara_flag = $has_chara_flag | $jouyo_flag;
        }
        
        print($result_string);

        $sql = "UPDATE user_data SET UserHasCharaFlag=".$has_chara_flag." where Id=".$input_userId;
        $stmh = $pdo->prepare($sql);
        $stmh->execute();
    
    } catch (PDOException $Exception) {
        print "エラー：" . $Exception->getMessage();
    }
}

?>