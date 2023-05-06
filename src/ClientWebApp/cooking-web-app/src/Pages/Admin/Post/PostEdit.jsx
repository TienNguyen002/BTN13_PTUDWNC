import React, {useRef, useState, useEffect} from "react";
import {Link, Navigate, useParams} from "react-router-dom"
import {
    addPost,
    getPostById,
    updatePost,
    updateImage
} from "../../../Services/PostRepository"
import { isEmptyOrSpaces, isInteger } from "../../../Utils/Utils"
import { Button, Form } from "react-bootstrap";
import { getPrices } from "../../../Services/Other"

const initialState = {
    title: "",
    shortDescription: "",
    description: "",
    metadata: "",
    urlSlug: "",
    author: {
      id: ""
    },
    category: {
      id: ""
    },
    authorId: "",
    categoryId: "",
    published: false
  };
  
  export default function PostEdit() {
    const params = useParams();
  
    const [post, setPosts] = useState(initialState);
    const [authors, setAuthors] = useState([]);
    const [categories, setCategories] = useState([]);
    const imageFile = useRef();
  
    useEffect(() => {
      fetchPosts();
      async function fetchPosts() {
        const data = await getPostById(params.id);
        if (data) {
          setPosts(data);
        } else setPosts([]);
        const authors = await getPrices();
        if (authors) setAuthors(authors);
        const categories = await getPrices();
        if (categories) setCategories(categories);
      }
    }, []);
  
    if (params.id && !isInteger(params.id)) {
      return <Navigate to={`/400?redirectTo=/admin/posts`} />;
    }
  
    const handleSubmit = (e) => {
      e.preventDefault();
      console.log(post);
      const data = {
        title: post.title,
        shortDescription: post.shortDescription,
        description: post.description,
        metadata: post.metadata,
        urlSlug: post.urlSlug,
        authorId: post.authorId ? post.authorId : post.author.id,
        categoryId: post.categoryId ? post.categoryId : post.category.id,
        published: post.published
      };
  
      console.log(data);
      const fileImage = imageFile.current.files[0];
      console.log(fileImage);
      if (params.id) {
        UpdatePost(data);
      } else {
        AddPost(data);
      }
  
      async function UpdatePost(data) {
        const res = await updatePost(params.id, data);
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
  
      async function AddPost(data) {
        const res = await addPost(data);
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
                value={post.title || ""}
                onChange={(e) =>
                  setPosts({
                    ...post,
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
                value={post.urlSlug || ""}
                onChange={(e) =>
                  setPosts({
                    ...post,
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
                value={post.shortDescription || ""}
                onChange={(e) =>
                  setPosts({
                    ...post,
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
                value={post.description || ""}
                onChange={(e) =>
                  setPosts({
                    ...post,
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
                value={post.metadata || ""}
                onChange={(e) =>
                  setPosts({
                    ...post,
                    metadata: e.target.value
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
                value={post.authorId ? post.authorId : post.author?.id}
                required
                onChange={(e) =>
                  setPosts({
                    ...post,
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
            <Form.Label className="col-sm-2 col-form-label">Chủ đề</Form.Label>
            <div className="col-sm-10">
              <Form.Select
                name="categoryId"
                title="CategoryId"
                value={post.categoryId ? post.categoryId : post.category?.id}
                required
                onChange={(e) =>
                  setPosts({
                    ...post,
                    categoryId: e.target.value
                  })
                }>
                <option value="">-- Chọn chủ đề --</option>
                {categories.map((category) => (
                  <option key={category.id} value={category.id}>
                    {category.name}
                  </option>
                ))}
              </Form.Select>
            </div>
          </div>
          {!isEmptyOrSpaces(post.imageUrl) && (
            <div className="row mb-3">
              <Form.Label className="col.sm-2 col-form-label">
                Hình hiện tại
              </Form.Label>
              <div className="col-sm-10">
                <img src={`https://localhost:7029/${post.imageUrl}`} alt="" />
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
                  setPosts({ ...post, imageFile: e.target.files[0] })
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
                  checked={post.published}
                  onChange={(e) =>
                    setPosts({ ...post, published: e.target.checked })
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
            <Link to="/admin/posts" className="btn btn-danger ms-2">
              Hủy và quay lại
            </Link>
          </div>
        </Form>
      </>
    );
}