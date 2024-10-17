-- INFORMATION TO NEW DEVELOPERS READING THIS FOR THE FIRST TIME

-- When adding a new microservice, please append your script for creating the database, user and granting priviliges at the end of the page.
-- If you need to patch an already existing microservice, please append any new script to ITS already defined section.
-- Make sure that any services that you add are clearly commented with labels as other services are.

-- If you have questions, please speak to your administrator.

-- ==============================
-- QUESTS SERVICE
-- ==============================

CREATE DATABASE IF NOT EXISTS Quests_db;
CREATE USER IF NOT EXISTS 'Quests_user'@'%' IDENTIFIED BY 'Quests-pass';
GRANT ALL PRIVILEGES ON Quests_db.* TO 'Quests_user'@'%';

-- ==============================
-- END OF SERVICES - APPLY CHANGES
-- ==============================
FLUSH PRIVILEGES;