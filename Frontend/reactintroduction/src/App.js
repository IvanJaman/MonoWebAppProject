import './App.css';
import { Button } from './Button';
import { Table } from './Table';
import { InputBar } from './InputBar';
import { useState, useEffect  } from 'react';
import axios from 'axios';

function App() {
  const [user, setUser] = useState({firstName: '', lastName: '' });
  const [users, setUsers] = useState([]);
  const [editId, setEditId] = useState(null);

  useEffect(() => {
    axios
      .get('https://localhost:7267/User/GetAll')
      .then(response => {
          console.log(response.data)
        setUsers(response.data);
        saveToLocalStorage(response.data);
      })
      .catch(error => {
        console.log('There was an error fetching the users!');
      });
  }, []);

  const saveToLocalStorage = (data) => {
    localStorage.setItem('users', JSON.stringify(data));
  };

  const loadFromLocalStorage = () => {
    const storedUsers = localStorage.getItem('users');
    if (storedUsers) {
      return JSON.parse(storedUsers);
    }
    return [];
  };

  useEffect(() => {
    setUsers(loadFromLocalStorage());
  }, []);

  const handleSubmit = () => {
    if(!user.firstName || !user.lastName){
      alert("Please provide a user.");
    }
    else{
      if (editId !== null) {
        const updatedTable = users.map(item =>
          item.id === editId
            ? { ...item, ...user }
            : item
        );
        setUsers(updatedTable);
        saveToLocalStorage(updatedTable);
        setEditId(null);
      } 
    else {
      const newData = { id: Date.now(), ...user};
      const updatedTable = [...users, newData];
      setUsers(updatedTable);
      saveToLocalStorage(updatedTable); 
    }
    setUser({ firstName: '', lastName: '' });
    }
  };

  const handleEdit = (id, firstName, lastName) => {
    setEditId(id);
    setUser({ firstName, lastName });
  };

  const handleDelete = (id) => {
    axios.delete(`http://localhost:7267/User/${id}`)
      .then(() => {
        const updatedTable = users.filter(item => item.id !== id);
        setUsers(updatedTable);
        saveToLocalStorage(updatedTable);
        console.log('User successfully deleted');
      })
      .catch(error => { 
        console.log('There was an error deleting the user!', error);
      });
  };

  return (
    <div className="App">
      <header className="App-header">
        <form>
          <div className="LoginDiv">
            <h1>Welcome to MyReactApp</h1>
            <h2>{editId ? 'Edit user info:' : 'Enter user info:'}</h2>
            <InputBar inputValue={user.firstName} setInputValue={(value) => setUser({ ...user, firstName: value })} placeholder="First Name" />
            <InputBar inputValue={user.lastName} setInputValue={(value)=> setUser({ ...user, lastName: value })} placeholder="Last Name" />
            <Button handleSubmit={handleSubmit} />
            <Table data={users} onEdit={handleEdit} onDelete={handleDelete}/>
          </div>
        </form>
      </header>
    </div>
  );
}

export default App;
