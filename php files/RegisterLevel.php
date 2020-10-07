<?php
//$servername = "localhost"; //use url if server is online
$servername = "database-aws.cimfp3oazumo.us-east-2.rds.amazonaws.com"; //use url if server is online
$username = "admin";

$dbname = "usersDB";

//variables submitted by user

$loginUser = $_POST["loginUser"];
$loginLevel = $_POST["loginLevel"];

// $loginUser = "rj";
// $loginLevel = 5;

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

// echo "Connected Successfully!"

$sql = "UPDATE `usersDB`.`users` SET `level` = ('" . $loginLevel . "') WHERE (`username` = '" . $loginUser . "' )";

$result = $conn->query($sql);
echo $result;

$conn->close();
?>