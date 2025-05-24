import React from "react";

interface SelectOptionProps {
  selectedGroup: string;
  onChange: (group: string) => void;
}

const options = [
  { value: "trongChuongTrinh", label: "Trong chương trình" },
  { value: "tuChon", label: "Tự chọn" },
  { value: "tuDo", label: "Tự do" },
];

const SelectOption: React.FC<SelectOptionProps> = ({
  selectedGroup,
  onChange,
}) => {
  return (
    <div className="flex gap-6">
      {options.map((opt) => (
        <label
          key={opt.value}
          className="flex items-center gap-2 cursor-pointer"
        >
          <input
            type="radio"
            name="group"
            value={opt.value}
            checked={selectedGroup === opt.value}
            onChange={() => onChange(opt.value)}
            className="accent-blue-700"
          />
          <span className="text-gray-800 font-medium">{opt.label}</span>
        </label>
      ))}
    </div>
  );
};

export default SelectOption;
