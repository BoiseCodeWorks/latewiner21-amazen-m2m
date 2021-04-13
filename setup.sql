USE timsburgers;
-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );

-- CREATE TABLE products
-- ( 
--   id INT NOT NULL AUTO_INCREMENT,
--   title VARCHAR(255) NOT NULL,
--   description VARCHAR(255),
--   price DECIMAL(6, 2) NOT NULL,
--   creatorId VARCHAR(255) NOT NULL, 
--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );


-- CREATE TABLE wishlists 
-- ( 
--   id INT NOT NULL AUTO_INCREMENT, 
--   title VARCHAR(255) NOT NULL,
--   creatorId VARCHAR(255) NOT NULL, 
--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );


-- CREATE TABLE wishlistproducts
-- (
--   id INT NOT NULL AUTO_INCREMENT, 
--   productId INT,
--   wishlistId INT,
--   creatorId VARCHAR(255),
--   PRIMARY KEY (id),

--    FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (productId)
--    REFERENCES products (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (wishlistId)
--    REFERENCES wishlists (id)
--    ON DELETE CASCADE

-- )
