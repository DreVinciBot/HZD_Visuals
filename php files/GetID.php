<?php
// $servername = "localhost"; //use url if server is online
// $username = "root";
// $password = "";
// $dbname = "arnavigationalstudy2020";

$servername = "database-aws.cimfp3oazumo.us-east-2.rds.amazonaws.com"; //use url if server is online
$username = "admin";
$password = "human-robot";
$dbname = "usersDB";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}

// $sql = "SELECT id FROM users";
$sql = "SELECT * FROM users WHERE id=(SELECT MAX(id) FROM users)";
$result = $conn->query($sql);
$last_id = $result->fetch_assoc();

// print_r($last_id["id"]);
echo($last_id["id"]);

$conn->close();
?>