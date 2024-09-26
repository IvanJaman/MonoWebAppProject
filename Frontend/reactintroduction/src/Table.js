import React from 'react';

export function Table({ data, onEdit, onDelete }) {
  const tableStyle = {
    backgroundColor: 'white',
  }

  const buttonStyle = {
    margin: '10px',
    borderRadius: '5px',
    cursor: 'pointer',
  };

  return (
    <table style={tableStyle} border="1">
      <thead>
        <tr>
          <th>Id</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Actions</th> {}
        </tr>
      </thead>
      <tbody>
        {data.map((row) => (
          <tr key={row.id}>
            <td>{row.id}</td>
            <td>{row.firstName}</td>
            <td>{row.lastName}</td>
            <td>
              <button style={buttonStyle} onClick={() => onEdit(row.id, row.firstName, row.lastName)}>
                Edit
              </button>
              <button style={buttonStyle} onClick={() => onDelete(row.id)}>
                Delete
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
