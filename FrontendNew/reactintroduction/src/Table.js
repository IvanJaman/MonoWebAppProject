import React from 'react';
import './App.css';

export function Table({ data, onEdit, onDelete }) {
  return (
    <>
    <form>
    <div className='Table'>
    <table border="1">
      <thead>
        <tr>
          <th>Id</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Sex</th>
          <th>Date of birth</th>
          <th>Actions</th> {}
        </tr>
      </thead>
      <tbody>
        {data.map((row) => (
          <tr key={row.id}>
            <td>{row.id}</td>
            <td>{row.firstName}</td>
            <td>{row.lastName}</td>
            <td>{row.sex}</td>
            <td>{row.dateOfBirth}</td>
            <td>
              <button onClick={() => onEdit(row.id, row.firstName, row.lastName, row.sex, row.dateOfBirth)}>
                Edit
              </button>
              <button onClick={() => onDelete(row.id)}>
                Delete
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
    </div>
    </form>
    </>
  );
}
