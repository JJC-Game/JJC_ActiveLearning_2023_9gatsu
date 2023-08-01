<?php
require_once 'CommonFunc.php';

$input_userId = $_GET['userId'];

try {
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

    $str = htmlspecialchars($row['Id']).","
    .htmlspecialchars($row['UserName']).","
    .htmlspecialchars($row['UserHasCharaFlag']);

    print($str);

  } catch (PDOException $Exception) {
      print "エラー：" . $Exception->getMessage();
  }

  $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $pdo->setAttribute(PDO::ATTR_EMULATE_PREPARES, false);
  //print "接続しました... <br>";
} catch(PDOException $Exception)
{
  //die('エラー :' . $Exception->getMessage());
}

?>
