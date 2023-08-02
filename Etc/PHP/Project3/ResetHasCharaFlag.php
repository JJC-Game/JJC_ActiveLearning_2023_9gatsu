<?php
ini_set( 'display_errors', 1 );
ini_set( 'error_reporting', E_ALL );

require_once 'CommonFunc.php';

$input_userId = $_GET['userId'];

$pdo = GetPDO();

if($pdo == null){
    exit(0);
}

try {
    $sql = "UPDATE user_data SET UserHasCharaFlag=0 where Id=".$input_userId;
    $stmh = $pdo->prepare($sql);
    $stmh->execute();

    print("UserId:".$input_userId."のキャラ所持フラグを消去");
} catch (PDOException $Exception) {
    print "エラー：" . $Exception->getMessage();
}
?>
