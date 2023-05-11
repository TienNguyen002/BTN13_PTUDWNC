import React, {useRef, useState, useEffect} from "react";
import {Link, Navigate, useParams} from "react-router-dom"
import {
    addRecipe,
    getRecipeById,
    updateRecipe,
    updateImage
} from "../../../Services/RecipeRepository"
import { isEmptyOrSpaces, isInteger } from "../../../Utils/Utils"
import { Button, Form } from "react-bootstrap";
import { getChefs } from "../../../Services/ChefRepository"
import { getAllCourses} from "../../../Services/CourseRepository"

const initialState = {
    title: "",
    shortDescription: "",
    description: "",
    metadata: "",
    urlSlug: "",
    ingredient: "",
    step: "",
    author: {
      id: ""
    },
    course: {
      id: ""
    },
    authorId: "",
    courseId: "",
    published: false
  };
  
  export default function RecipeEdit() {
    const params = useParams();
  
    const [recipe, setRecipes] = useState(initialState);
    const [authors, setAuthors] = useState([]);
    const [courses, setCourses] = useState([]);
    const imageFile = useRef();
  
    useEffect(() => {
      fetchCourses();
      async function fetchCourses() {
        const data = await getRecipeById(params.id);
        if (data) {
          setRecipes(data);
        } else setRecipes([]);
        const authors = await getChefs();
        if (authors) setAuthors(authors);
        const courses = await getAllCourses();
        if (courses) setCourses(courses);
      }
    }, []);
  
    if (params.id && !isInteger(params.id)) {
      return <Navigate to={`/400?redirectTo=/admin/recipes`} />;
    }
  
    const handleSubmit = (e) => {
      e.preventDefault();
      console.log(recipe);
      const data = {
        title: recipe.title,
        shortDescription: recipe.shortDescription,
        description: recipe.description,
        metadata: recipe.metadata,
        urlSlug: recipe.urlSlug,
        ingredient: recipe.ingredient,
        step: recipe.step,
        authorId: recipe.authorId ? recipe.authorId : recipe.author.id,
        courseId: recipe.courseId ? recipe.courseId : recipe.course.id,
        published: recipe.published
      };
  
      console.log(data);
      const fileImage = imageFile.current.files[0];
      console.log(fileImage);
      if (params.id) {
        UpdateRecipe(data);
      } else {
        AddRecipe(data);
      }
  
      async function UpdateRecipe(data) {
        const res = await updateRecipe(params.id, data);
        console.log(res);
        if (res) {
          alert("Cập nhập thành công");
        } else {
          console.log(res);
          alert("Cập nhập thất bại");
        }
        const resImage = await updateImage(res.id, fileImage)
        if (resImage) {
          alert("Cập nhập hình ảnh thành công");
        } else {
          console.log(resImage);
          alert("Cập nhập hình ảnh thất bại");
        }
      }
  
      async function AddRecipe(data) {
        const res = await addRecipe(data);
        if (res) {
          alert("Thêm thành công");
        } else {
          alert("Thêm thất bại");
        }
      }
    };
  
    return (
      <>
        <Form
          method={isInteger(params.id) ? "put" : "post"}
          encType="multipart/form-data"
          onSubmit={handleSubmit}>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">Tiêu đề</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="text"
                name="title"
                title="Title"
                required
                value={recipe.title || ""}
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    title: e.target.value
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">UrlSlug</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="text"
                name="slug"
                title="Slug"
                required
                value={recipe.urlSlug || ""}
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    urlSlug: e.target.value
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">
              Giới thiệu
            </Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="text"
                name="shortDescription"
                title="Short Description"
                required
                value={recipe.shortDescription || ""}
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    shortDescription: e.target.value
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">Mô tả</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="text"
                name="description"
                title="Description"
                required
                value={recipe.description || ""}
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    description: e.target.value
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">Metadata</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="text"
                name="metadata"
                title="Metadata"
                required
                value={recipe.metadata || ""}
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    metadata: e.target.value
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">Nguyên liệu</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="textarea"
                row={6}
                name="ingredient"
                title="Ingredient"
                required
                value={recipe.ingredient || ""}
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    ingredient: e.target.value
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">Các bước thực hiện</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                as={'textarea'}
                type="text"
                name="step"
                title="Step"
                required
                value={recipe.step || ""}
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    step: e.target.value
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">Tác giả</Form.Label>
            <div className="col-sm-10">
              <Form.Select
                name="authorId"
                title="AuthorId"
                value={recipe.authorId ? recipe.authorId : recipe.author?.id}
                required
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    authorId: e.target.value
                  })
                }>
                <option value="">-- Chọn tác giả --</option>
                {authors.map((author) => (
                  <option key={author.id} value={author.id}>
                    {author.fullName}
                  </option>
                ))}
              </Form.Select>
            </div>
          </div>
          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">Khóa học</Form.Label>
            <div className="col-sm-10">
              <Form.Select
                name="courseId"
                title="CourseId"
                value={recipe.courseId ? recipe.courseId : recipe.course?.id}
                required
                onChange={(e) =>
                  setRecipes({
                    ...recipe,
                    courseId: e.target.value
                  })
                }>
                <option value="">-- Chọn khóa học --</option>
                {courses.map((course) => (
                  <option key={course.id} value={course.id}>
                    {course.title}
                  </option>
                ))}
              </Form.Select>
            </div>
          </div>
          {!isEmptyOrSpaces(recipe.imageUrl) && (
            <div className="row mb-3">
              <Form.Label className="col.sm-2 col-form-label">
                Hình hiện tại
              </Form.Label>
              <div className="col-sm-10">
                <img src={`https://localhost:7029/${recipe.imageUrl}`} alt="" />
              </div>
            </div>
          )}
          <div className="row mb-3">
            <Form.Label className="col.sm-2 col-form-label">
              Chọn hình ảnh
            </Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="file"
                name="imageFile"
                accept="image/*"
                title="Image File"
                ref={imageFile}
                onChange={(e) =>
                  setRecipes({ ...recipe, imageFile: e.target.files[0] })
                }
              />
            </div>
          </div>
          <div className="row mb-3">
            <div className="col-sm-10 offset-sm-2">
              <div className="form-check">
                <input
                  className="form-check-input"
                  type="checkbox"
                  name="published"
                  checked={recipe.published}
                  onChange={(e) =>
                    setRecipes({ ...recipe, published: e.target.checked })
                  }
                />
                <Form.Label className="form-check-label">Đã xuất bản</Form.Label>
              </div>
            </div>
          </div>
          <div className="text-center">
            <Button variant="primary" type="submit">
              Lưu các thay đổi
            </Button>
            <Link to="/admin/recipes" className="btn btn-danger ms-2">
              Hủy và quay lại
            </Link>
          </div>
        </Form>
      </>
    );
}