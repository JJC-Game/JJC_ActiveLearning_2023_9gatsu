<?php
ini_set( 'display_errors', 1 );
ini_set( 'error_reporting', E_ALL );

require_once 'CommonFunc.php';

$input_userId = $_GET['userId'];

PlayGacha($input_userId, 1);
?>
