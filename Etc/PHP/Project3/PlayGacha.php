<?php
ini_set( 'display_errors', 1 );
ini_set( 'error_reporting', E_ALL );

require_once 'CommonFunc.php';

$input_userId = $_GET['userId'];

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

PlayGacha($input_userId, 1);
?>
