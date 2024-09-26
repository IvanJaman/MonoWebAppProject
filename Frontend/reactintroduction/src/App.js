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
      })
      .catch(error => {
        console.log('There was an error fetching the users!');
      });
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
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
        setEditId(null);
      } 
      else {
        const newData = { id: Date.now(), ...user};
        const updatedTable = [...users, newData];
        setUsers(updatedTable);
      }
      setUser({ firstName: '', lastName: '' });
    }
  };

  const handleEdit = (id, firstName, lastName) => {
    axios
      .put(`http://localhost:7267/User/UpdateUser/${id}`)
      .then(() => {
        setEditId(id);
        setUser({ firstName, lastName });
        handleSubmit();
        console.log('User successfully updated!');
      })
      .catch(error => { 
        console.log('There was an error updating the user!', error);
      });
  };

  async function handleDelete (id) {
    try{
      debugger;
    const response=await axios.delete(`https://localhost:7267/User/DeleteUser`,{
      params: { id }});
      const test= response.data;
        debugger;
       
        console.log('User successfully deleted!');
      }
      catch(error) { 
        console.log('There was an error deleting the user!', error);
      };
  };

  return (
    <div className="App">
      <header className="App-header">
        <form onSubmit={handleSubmit}>
          <div className="LoginDiv">
            <h1>Welcome to MyReactApp</h1>
            <h2>{editId ? 'Edit user info:' : 'Enter user info:'}</h2>
            <InputBar inputValue={user.firstName} setInputValue={(value) => setUser({ ...user, firstName: value })} placeholder="First Name" />
            <InputBar inputValue={user.lastName} setInputValue={(value)=> setUser({ ...user, lastName: value })} placeholder="Last Name" />
            <Button />
            <Table data={users} onEdit={handleEdit} onDelete={handleDelete}/>
          </div>
        </form>
      </header>
    </div>
  );
}

export default App;
