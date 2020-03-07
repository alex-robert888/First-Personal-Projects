
from Repository.Repository import Repository
import mysql.connector

class SQLBasedRepository(Repository):
    def __init__(self, table_name):
        self.table_name = table_name

        super().__init__()
        self.conn = mysql.connector.connect( host='localhost',
                                             user= "root",
                                             password= "mysqlfacultate1234",
                                             database= "movie_rentals"
                                             )

        if self.conn.is_connected():
            db_Info = self.conn.get_server_info()
            print("Connected to MySQL Server version ", db_Info)
            self.cursor = self.conn.cursor()
            self.cursor.execute("select database();")
            record = self.cursor.fetchone()
            print("You're connected to database: ", record)

    def write_data_to_mysql(self):
        if self.table_name == "clients":
            self.cursor.execute("DELETE FROM clients")
            for e in self.repo:
                print(e)
                cmd = "INSERT INTO clients(id, name) VALUES(%s, %s)"
                self.cursor.execute(cmd, (e.id, e.name))
        elif self.table_name == "movies":
            self.cursor.execute("DELETE FROM movies")
            for e in self.repo:
                cmd = "INSERT INTO movies VALUES(%s, %s, %s, %s)"
                self.cursor.execute(cmd, (e.id, e.title, e.description, e.genre))
        else:
            self.cursor.execute("DELETE FROM rentals")
            for e in self.repo:
                cmd = "INSERT INTO rentals VALUES(%s, %s, %s, %s, %s, %s)"
                self.cursor.execute(cmd, (e.id, e.client_id, e.movie_id, e.rented_date, e.due_date, e.return_date))

        self.conn.commit()

    def read_data_from_mysql(self):
        if self.table_name == "clients":
            self.cursor.execute("SELECT * FROM clients")
        elif self.table_name == "movies":
            self.cursor.execute("SELECT * FROM movies")
        else:
            self.cursor.execute("SELECT * FROM rentals")

        all = self.cursor.fetchall()
        return all