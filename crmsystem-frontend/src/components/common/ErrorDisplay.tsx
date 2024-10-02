import React from 'react';

interface ErrorDisplayProps {
  error: string | null;
  validationErrors?: Record<string, string[]> | null;
}

const ErrorDisplay: React.FC<ErrorDisplayProps> = ({ error, validationErrors }) => {
  if (!error && !validationErrors) return null;

  return (
    <div className="mb-4">
      {error && <p className="text-red-500">{error}</p>}
      {validationErrors && (
        <ul className="text-red-500 list-disc list-inside">
          {Object.entries(validationErrors).map(([field, errors]) => (
            <li key={field}>{field}: {errors.join(', ')}</li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default ErrorDisplay;