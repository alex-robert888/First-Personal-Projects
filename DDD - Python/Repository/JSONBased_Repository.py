

from Repository.Repository import Repository
import json

class JSONBasedRepository(Repository):
    def __init__(self, json_file_name):
        super().__init__()
        self.json_file_name = json_file_name

    def read_data_from_json(self):
        with open(self.json_file_name) as json_file:
            data = json.load(json_file)
            json_file.close()
            return data

    def write_data_to_json(self, entity_type):
        data = dict()
        data[entity_type] = list()

        if entity_type == "clients":
            for e in self.repo:
                data[entity_type].append({
                    "id": int(e.id),
                    "name": e.name
                })

        elif entity_type == "movies":
            for e in self.repo:
                data[entity_type].append({
                    "id": int(e.id),
                    "title": e.title,
                    "description": e.description,
                    "genre": e.genre
                })

        elif entity_type == "rentals":
            for e in self.repo:
                data[entity_type].append({
                    "id": int(e.id),
                    "client_id": e.client_id,
                    "movie_id": e.movie_id,
                    "rented_date": str(e.rented_date),
                    "due_date": str(e.due_date),
                    "return_date": str(e.return_date)

                })

        with open(self.json_file_name, 'w') as fout:
            json.dump(data, fout, indent=2)

