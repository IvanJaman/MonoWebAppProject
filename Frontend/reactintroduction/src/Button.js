import React from 'react';

export function Button({ handleSubmit }) {
  const buttonStyle = {
    padding: '5px 10px',
    margin: '10px',
    border: '1px solid black',
    borderRadius: '5px',
    backgroundColor: 'white',
    color: 'black',
    cursor: 'pointer',
    fontSize: '16px'
  };

  return (
    <button style={buttonStyle} onClick={handleSubmit}>
      Submit
    </button>
  );
}
