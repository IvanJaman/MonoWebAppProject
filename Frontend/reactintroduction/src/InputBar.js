import React from 'react';

export function InputBar({ inputValue, setInputValue, placeholder }) {
  const handleChange = (event) => {
    setInputValue(event.target.value);
  };

  return (
    <div>
      <input
        type="text"
        value={inputValue}
        onChange={handleChange}
        placeholder={placeholder}
      />
    </div>
  );
}
