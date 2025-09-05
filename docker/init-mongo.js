db = db.getSiblingDB('SmartContacts');

db.createUser({
  user: 'smartcontacts_user',
  pwd: 'smartcontacts_pass',
  roles: [
    {
      role: 'readWrite',
      db: 'SmartContacts'
    }
  ]
});

db.createCollection('contacts');

// Insert sample data
db.contacts.insertMany([
  {
    _id: ObjectId(),
    Name: {
      FirstName: "João",
      LastName: "Silva"
    },
    Address: {
      Street: "Rua das Flores",
      Number: "123",
      District: "Jardim Paulista",
      City: "São Paulo"
    },
    Phone: {
      CountryCode: "55",
      AreaCode: "11",
      Number: "999999999"
    },
    CreatedAt: new Date(),
    UpdatedAt: new Date()
  },
  {
    _id: ObjectId(),
    Name: {
      FirstName: "Maria",
      LastName: "Santos"
    },
    Address: {
      Street: "Avenida Paulista",
      Number: "456",
      District: "Bela Vista",
      City: "São Paulo"
    },
    Phone: {
      CountryCode: "55",
      AreaCode: "11",
      Number: "888888888"
    },
    CreatedAt: new Date(),
    UpdatedAt: new Date()
  }
]);